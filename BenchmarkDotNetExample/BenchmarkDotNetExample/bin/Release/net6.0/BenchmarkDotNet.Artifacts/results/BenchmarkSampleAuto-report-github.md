``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


```
| Method |      Mean |    Error |   StdDev |  Gen 0 | Allocated |
|------- |----------:|---------:|---------:|-------:|----------:|
|  first | 165.18 ns | 1.804 ns | 1.688 ns | 0.0219 |     184 B |
| second |  44.65 ns | 0.440 ns | 0.390 ns | 0.0220 |     184 B |
|  third |  85.99 ns | 1.714 ns | 1.604 ns | 0.0334 |     280 B |
