# Simple Calculator API (ASP.NET Core)

This API returns JSON only, there are no Razor views or other GUI elements.

### Inputs 
- Operation: add, sub, div, mul
- Operands: Any number of numeric (float/single) operands, separated by semicolons

### Output (JSON)
- Result: Float/single number
- Error: If inputs are missing or invalid

### Examples
- api/calculator/add/3;5;2 <strong>returns</strong> 10.0
- api/calculator/mul/2;5;4;3 <strong>returns</strong> 120.0

### Usage

Clone the repo, open the .sln file with visual studio and run debug with either IIS Express or Docker.

A web browser tab should open for you automatically, pointing to api/calculator and returning a list of valid operations.

You may now run your queries!
