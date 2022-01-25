using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ListArray_Benchmarks;

internal static class AdditionalTests {


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
