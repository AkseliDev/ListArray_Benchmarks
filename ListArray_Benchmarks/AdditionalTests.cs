using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ListArray_Benchmarks;

internal static class AdditionalTests {


    /// <summary>
    /// Test to see if the garbage collector moves the array on a forced GC
    /// </summary>
    unsafe public static void Theory_Does_GC_Move_OnForced() {

        /**
         * Observations:
         * Confirmed, the GC does move the array sometimes on smaller sizes
         * But on a larger array (1000000+ elements) (assuming Large Objects) the GC does not seem to move it
         */
        const int ArraySize = 1_000_000;
        const int Tests = 10;

        // lets create a large array
        int[] array = new int[ArraySize];

        Span<ulong> addresses = stackalloc ulong[Tests];
        
        for(int i = 0; i < addresses.Length; i++) {

            // get the current base address
            addresses[i] = (ulong)Unsafe.AsPointer(ref array[0]);

            // force garbage collection to run, and attempt to move the memory
            System.GC.Collect();
        }

        // lets print the base addresses and check if they are all the same
        ulong firstAddress = addresses[0];
        bool areSame = true;
        for (int i = 0; i < addresses.Length; i++) {
            ulong address = addresses[i];
            Console.WriteLine($"{{{i}}} address: {address}");
            if (address != firstAddress) {
                areSame = false;
            }
        }

        Console.WriteLine($"All addresses match: {areSame}");

    }

    /// <summary>
    /// Method to ensure that fixed works for lists (via <see cref="CollectionsMarshal.AsSpan"/>)
    /// </summary>
    public static void Ensure_FixedWorks_List() {

        // create a list
        var list = new List<int>();

        // add some elements to it
        for(int i = 0; i < 10; i++) {
            list.Add(i);
        }

        // lets print the list to be sure what it looks like
        Console.WriteLine( string.Join(",", list) );


        // we get the span using CollectionsMarshal
        Span<int> span = CollectionsMarshal.AsSpan(list);

        unsafe {

            // we use fixed statement on the span to get the pointer
            fixed(int* pointer = span) {

                // we initiate a garbage collection to try make the GC move the lists internal array
                System.GC.Collect();

                // lets log the stuff
                for(int i = 0; i < span.Length; i++) {
                    Console.Write($"{pointer[i]},");
                }
            }
        }

        // then we can observe the results in console

        /**
         * @compiled-result:
         * 
         * 0,1,2,3,4,5,6,7,8,9
         * 0,1,2,3,4,5,6,7,8,9
         * 
         * Conclusion: Fixed works as intended on a list via CollectionsMarshal
         */
    }

}
