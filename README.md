# ListArray Benchmarks

Benchmarks of multiple list and array methods to compare performance differences.

## Term Explanation
* _ForEach  => loop using `foreach`
* _For      => loop using `for`
* _Local    => the reference is placed on a local variable
* _Span     => the reference is assigned to a span
* Unsafe    => the method uses `unsafe` code
* _Reverse  => loop is done in reverse (constant 0 comparison vs length comparison)
* _AddrLT   => address less than comparison instead of length comparison

## NET 6.0 x64 - i7-9800X CPU 3.80GHz

### 1 000 000 items
|                        Method |     Mean |   Error |   StdDev |   Median |
|:------------------------------|---------:|--------:|---------:|---------:|
|                  List_ForEach | 740.1 us | 1.04 us |  0.92 us | 739.9 us |
|            List_ForEach_Local | 741.7 us | 3.27 us |  2.90 us | 740.5 us |
|                      List_For | 569.0 us | 0.48 us |  0.40 us | 568.9 us |
|                List_For_Local | 585.1 us | 0.95 us |  0.84 us | 584.8 us |
|                 List_For_Span | 517.3 us | 0.25 us |  0.22 us | 517.3 us |
|                 Array_ForEach | 363.5 us | 0.71 us |  0.63 us | 363.5 us |
|           Array_ForEach_Local | 363.1 us | 0.35 us |  0.27 us | 363.0 us |
|                     Array_For | 519.8 us | 0.87 us |  0.77 us | 519.5 us |
|               Array_For_Local | 517.4 us | 0.31 us |  0.29 us | 517.3 us |
|                Array_For_Span | 514.0 us | 1.39 us |  1.30 us | 513.9 us |
| ArrayUnsafe_For_Fixed_Reverse | 350.6 us | 2.61 us |  2.18 us | 349.7 us |
|  ArrayUnsafe_For_Fixed_AddrLT | 321.0 us | 6.40 us | 10.33 us | 315.0 us |
|  ListUnsafe_For_Fixed_Reverse | 337.0 us | 0.81 us |  0.71 us | 336.7 us |
|   ListUnsafe_For_Fixed_AddrLT | 314.8 us | 0.83 us |  0.65 us | 314.6 us |

### 10 000 items
|                        Method |     Mean |     Error |    StdDev |
|------------------------------ |---------:|----------:|----------:|
|                  List_ForEach | 7.456 us | 0.0937 us | 0.0830 us |
|            List_ForEach_Local | 7.423 us | 0.0252 us | 0.0223 us |
|                      List_For | 5.724 us | 0.0398 us | 0.0373 us |
|                List_For_Local | 5.841 us | 0.0026 us | 0.0020 us |
|                 List_For_Span | 4.608 us | 0.0284 us | 0.0252 us |
|                 Array_ForEach | 3.645 us | 0.0156 us | 0.0138 us |
|           Array_ForEach_Local | 3.675 us | 0.0645 us | 0.0634 us |
|                     Array_For | 5.200 us | 0.0080 us | 0.0067 us |
|               Array_For_Local | 5.179 us | 0.0045 us | 0.0038 us |
|                Array_For_Span | 5.108 us | 0.0078 us | 0.0069 us |
| ArrayUnsafe_For_Fixed_Reverse | 3.404 us | 0.0215 us | 0.0179 us |
|  ArrayUnsafe_For_Fixed_AddrLT | 3.154 us | 0.0021 us | 0.0019 us |
|  ListUnsafe_For_Fixed_Reverse | 3.164 us | 0.0038 us | 0.0034 us |
|   ListUnsafe_For_Fixed_AddrLT | 3.151 us | 0.0044 us | 0.0041 us |
