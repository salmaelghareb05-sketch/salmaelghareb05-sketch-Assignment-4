
## Benchmark Overview

This benchmark compares two approaches for repeatedly building a string:

* `StringConcatenation` using the `+` operator.
* `StringBuilderConcatenation` using `StringBuilder.AppendLine()`.

The benchmark uses BenchmarkDotNet with `MemoryDiagnoser` to compare execution time and memory allocation.

## Benchmark Results

The benchmark was executed on my machine.

| Method                     | Iterations |         Mean |      Gen0 |     Gen1 |   Allocated |
| -------------------------- | ---------: | -----------: | --------: | -------: | ----------: |
| StringConcatenation        |        100 |    19.675 us |   62.7441 |   0.9155 |    288.4 KB |
| StringBuilderConcatenation |        100 |     1.395 us |    3.1528 |   0.1011 |    14.52 KB |
| StringConcatenation        |       1000 | 1,839.509 us | 6142.5781 | 855.4688 | 28372.97 KB |
| StringBuilderConcatenation |       1000 |     9.683 us |   26.3062 |   5.2490 |   122.41 KB |

## Analysis

### 1. Which approach was faster with 100 iterations?

With 100 iterations, `StringBuilderConcatenation` was faster.

* String: **19.675 us**
* StringBuilder: **1.395 us**

### 2. Which approach was faster with 1000 iterations?

With 1000 iterations, `StringBuilderConcatenation` was also much faster.

* String: **1,839.509 us**
* StringBuilder: **9.683 us**

The difference became much larger as the number of iterations increased.

### 3. Which approach allocated more memory?

`StringConcatenation` allocated significantly more memory.

For 1000 iterations:

* String: **28,372.97 KB**
* StringBuilder: **122.41 KB**

This shows a large difference in memory allocation.

### 4. What happened to string concatenation performance as the loop size increased?

As the number of iterations increased, normal string concatenation became much slower.

For example:

* At 100 iterations: **19.675 us**
* At 1000 iterations: **1,839.509 us**

The execution time increased significantly.

### 5. Why does repeated string concatenation create additional allocations?

In C#, `string` is immutable. This means that when a string is changed, the existing string cannot be modified directly.

new string objects can be created as the result grows. This causes additional memory allocations and copying of string data.

### 6. Why is StringBuilder usually better when repeatedly appending?

`StringBuilder` is designed for building and modifying text repeatedly.

Instead of creating a new string for every append operation, it maintains an internal buffer that can grow as needed.

This makes it more suitable for scenarios where many pieces of text are appended repeatedly.

The benchmark results demonstrate this difference clearly, especially at 1000 iterations.

### 7. Is StringBuilder always better than normal string operations?

No.

For simple operations involving a small number of strings, normal string operations can be clearer and more convenient.

`StringBuilder` becomes more useful when text is being modified or appended repeatedly, especially inside loops or when constructing large strings.

## Conclusion

Based on the benchmark results from my machine, `StringBuilder` performed significantly better than repeated string concatenation in this repeated-append scenario.

It was faster and allocated considerably less memory at both 100 and 1000 iterations.

The difference became much more noticeable as the number of iterations increased.

The benchmark demonstrates why `StringBuilder` can be a better choice when repeatedly building large strings.
