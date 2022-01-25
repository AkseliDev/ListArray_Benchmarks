using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ListArray_Benchmarks;
using System.Runtime.InteropServices;


// AdditionalTests.Ensure_FixedWorks_List();

BenchmarkRunner.Run<Benchmarking>();


public class Benchmarking {

    const int ArraySize = 1_000_000;


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
}