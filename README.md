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
|                        Method |     Mean |   Error |  StdDev |
|:------------------------------|---------:|--------:|--------:|
|                  List_ForEach | 741.3 us | 5.00 us | 4.18 us |
|            List_ForEach_Local | 739.3 us | 0.62 us | 0.49 us |
|                      List_For | 568.4 us | 0.38 us | 0.32 us |
|                List_For_Local | 585.5 us | 0.83 us | 0.65 us |
|                 List_For_Span | 517.6 us | 0.53 us | 0.47 us |
|                 Array_ForEach | 363.1 us | 0.14 us | 0.11 us |
|           Array_ForEach_Local | 363.3 us | 0.20 us | 0.17 us |
|                     Array_For | 519.9 us | 0.26 us | 0.22 us |
|               Array_For_Local | 517.6 us | 0.29 us | 0.26 us |
|                Array_For_Span | 512.4 us | 1.36 us | 1.27 us |
| ArrayUnsafe_For_Fixed_Reverse | 349.8 us | 2.65 us | 2.83 us |
|  ArrayUnsafe_For_Fixed_AddrLT | 314.1 us | 0.21 us | 0.19 us |
|  ListUnsafe_For_Fixed_Reverse | 339.9 us | 0.53 us | 0.50 us |
|   ListUnsafe_For_Fixed_AddrLT | 314.1 us | 0.15 us | 0.14 us |

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


## NET 6.0 x64 - AMD Ryzen 7 3700U

### 1 000 000 items
|                        Method |       Mean |    Error |    StdDev |
|:------------------------------|-----------:|---------:|----------:|
|                  List_ForEach | 1,377.7 us | 56.00 us | 159.76 us |
|            List_ForEach_Local | 1,254.3 us | 60.00 us | 176.90 us |
|                      List_For |   875.0 us | 87.39 us | 257.67 us |
|                List_For_Local |   653.7 us | 12.28 us |  24.80 us |
|                 List_For_Span |   377.1 us |  6.76 us |   6.32 us |
|                 Array_ForEach |   394.0 us |  6.98 us |   6.19 us |
|           Array_ForEach_Local |   394.9 us |  7.83 us |   9.90 us |
|                     Array_For |   584.7 us | 11.55 us |  25.35 us |
|               Array_For_Local |   371.8 us |  1.53 us |   1.20 us |
|                Array_For_Span |   376.1 us |  6.96 us |   6.17 us |
| ArrayUnsafe_For_Fixed_Reverse |   393.9 us |  7.28 us |   6.45 us |
|  ArrayUnsafe_For_Fixed_AddrLT |   407.6 us |  7.68 us |   9.44 us |
|  ListUnsafe_For_Fixed_Reverse |   784.3 us | 43.65 us | 128.71 us |
|   ListUnsafe_For_Fixed_AddrLT |   622.7 us | 15.96 us |  47.06 us |
