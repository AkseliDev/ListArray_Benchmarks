using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ListArray_Benchmarks;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;


// AdditionalTests.Ensure_FixedWorks_List();
// AdditionalTests.Theory_Does_GC_Move_OnForced();

BenchmarkRunner.Run<Benchmarking>();

Console.ReadKey();
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();

public class Benchmarking {

    const int ArraySize = 1_000_000;

    /**
     * (?) Add multiple lists and arrays to ensure fair cpu cache? -unsure if matters
     * 
     * @see AdditionalTests::Theory_Does_GC_Move_OnForced
     */

    List<int> intList;
    int[] intArray;


    public Benchmarking() {
        intList = Enumerable.Range(0, ArraySize).ToList();
        intArray = intList.ToArray();
    }
    
    
    #region List Methods

    [Benchmark]
    public int List_ForEach() {
        int result = 0;
        foreach (int i in intList) {
            result += i;
        }
        return result;
    }

    [Benchmark]
    public int List_ForEach_Local() {
        var localPin = intList;
        int result = 0;
        foreach (int i in localPin) {
            result += i;
        }
        return result;
    }

    [Benchmark]
    public int List_For() {
        int result = 0;
        for(int i = 0; i < intList.Count; i++) {
            result += intList[i];
        }
        return result;
    }

    [Benchmark]
    public int List_For_Local() {
        var localPin = intList;
        int result = 0;
        for (int i = 0; i < localPin.Count; i++) {
            result += localPin[i];
        }
        return result;
    }

    [Benchmark]
    public int List_For_Span() {
        Span<int> span = CollectionsMarshal.AsSpan(intList);
        int result = 0;
        for (int i = 0; i < span.Length; i++) {
            result += span[i];
        }
        return result;
    }

    #endregion


    #region Array Methods
    
    [Benchmark]
    public int Array_ForEach() {
        int result = 0;
        foreach (int i in intArray) {
            result += i;
        }
        return result;
    }
    
    [Benchmark]
    public int Array_ForEach_Local() {
        var localPin = intArray;
        int result = 0;
        foreach (int i in localPin) {
            result += i;
        }
        return result;
    }

    [Benchmark]
    public int Array_For() {
        int result = 0;
        for (int i = 0; i < intArray.Length; i++) {
            result += intArray[i];
        }
        return result;
    }

    [Benchmark]
    public int Array_For_Local() {
        var localPin = intArray;
        int result = 0;
        for (int i = 0; i < localPin.Length; i++) {
            result += localPin[i];
        }
        return result;
    }

    [Benchmark]
    public int Array_For_Span() {
        Span<int> span = intArray;
        int result = 0;
        for (int i = 0; i < span.Length; i++) {
            result += span[i];
        }
        return result;
    }

    
    #endregion
    
    #region Unsafe Methods
    [Benchmark]
    public int ArrayUnsafe_For_Fixed_Reverse() {
        int result = 0;
        unsafe {
            fixed (int* arr = intArray) {
                int* pointer = arr;
                for (int i = intArray.Length; i > 0; i--) {
                    result += *pointer;
                    pointer++;
                }
            }
        }
        return result;
    }
    
    [Benchmark]
    public int ArrayUnsafe_For_Fixed_AddrLT() {
        int result = 0;
        unsafe {
            fixed (int* arr = intArray) {
                int* ptr = arr;
                int* endPtr = arr + intArray.Length;
                while (ptr < endPtr) {
                    result += *ptr;
                    ptr++;
                }
            }
        }
        return result;
    }
    
    [Benchmark]
    public int ListUnsafe_For_Fixed_Reverse() {

        Span<int> span = CollectionsMarshal.AsSpan(intList);

        int result = 0;

        unsafe {
            fixed (int* arr = span) {
                int* pointer = arr;
                for (int i = span.Length; i > 0; i--) {
                    result += *pointer;
                    pointer++;
                }
            }
        }
        return result;
    }

    [Benchmark]
    public int ListUnsafe_For_Fixed_AddrLT() {

        Span<int> span = CollectionsMarshal.AsSpan(intList);

        int result = 0;

        unsafe {
            fixed (int* arr = span) {
                int* ptr = arr;
                int* endPtr = arr + span.Length;
                while (ptr < endPtr) {
                    result += *ptr;
                    ptr++;
                }
            }
        }
        return result;
    }
    #endregion
    
    [Benchmark]
    public int ArrayUnsafe_Unrolled() {

        int result = 0;
        unsafe {
            fixed (int* arr = intArray) {
                int* pointer = arr;
                int length = intArray.Length;
                while(length >= 8) {
                    result += pointer[0];
                    result += pointer[1];
                    result += pointer[2];
                    result += pointer[3];
                    result += pointer[4];
                    result += pointer[5];
                    result += pointer[6];
                    result += pointer[7];
                    pointer += 8;
                    length -= 8;
                }
                while(length-- > 0) {
                    result += pointer[0];
                    pointer++;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// <see href="https://docs.microsoft.com/en-us/dotnet/standard/simd"/>
    /// </summary>
    [Benchmark]
    public int Array_SimdVectorization() {

        var pin = intArray;

        int size = Vector<int>.Count;
        var vector = Vector<int>.Zero;

        int i;
        int end = pin.Length - size;

        for (i = 0; i <= end; i += size) {
            vector = Vector.Add(vector, new Vector<int>(pin, i));
        }

        int result = Vector.Dot(vector, Vector<int>.One);
        for(; i < pin.Length; i++) {
            result += pin[i];
        }

        return result;
    }
    
    // new impl, using Sum instead of Dot product, seems to be faster
    // i dont know why the initial author used dot product.
    [Benchmark]
    public int Array_SimdVectorization2() {

        var pin = intArray;

        int size = Vector<int>.Count;
        var vector = Vector<int>.Zero;

        int i;
        int end = pin.Length - size;

        for (i = 0; i <= end; i += size) {
            vector = Vector.Add(vector, new Vector<int>(pin, i));
        }

        int result = Vector.Sum(vector);
        for (; i < pin.Length; i++) {
            result += pin[i];
        }

        return result;
    }
}
