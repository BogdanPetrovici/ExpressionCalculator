# ExpressionCalculator
Simple console application for computing results to arithmetic expressions

# Usage
When prompted, write the desired expression without any whitespaces and press the Return key.
If the expression can be evaluated the result will be displayed below, otherwise an error message will be displayed.

# Allowed operations:
- addition: e.g. 5+4
- substraction: e.g. 1.5-0.25
- multiplication: e.g. 1.2*3
- division: e.g: 3/2
- exponentiation: e.g. 5^2
- association: e.g. (1+3)*(2^2-1)

  Numbers can be floating-point, the decimal separator is the 'period' character ('.'). Both intermediate results and the final result of the computation must fit within the .NET double range (±5.0 × 10^−324 to ±1.7 × 10^308).
