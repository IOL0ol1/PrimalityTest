# PrimalityTest

primality test (Miller Rabin) implement by C#,     
transplantation from a C Language Library [Mpir](https://github.com/coapp-packages/mpir)    


## Features

1. use System.Numerics.BigInteger (NET4 and later) support any size integer (Memory allowable).      
2. for input < 1000000, use determinate algorithm, The result is accurate.    

## How to

1. import system.numerics.dll and add BigIntegerEx.cs file to your project    

2. using namespace
```charp
using System.Numerics;
```

3.
**Sample 1**
```csharp
string input = "12345678656434565342324354651234567865643456534232435465";
/// 23 is Miller Rabin loop count
bool isPrime = BigInteger.Parse(input).IsPrime(23, out double a);
if(isPrime)
    Trace.TraceInformation($"The probability of a prime number is {a}.");
else
    Trace.TraceInformation("This is NOT prime.");
```
**Sample 2**
```csharp
string input = "12345678656434565342324354651234567865643456534232435465";
int result = BigIntegerEx.IsPrime(BigInteger.Parse(input), 23);
switch (result)
{
    case 0:
        Trace.TraceInformation("This is NOT prime.");
        break;
    case 1:
        Trace.TraceInformation("This is 'probably' prime");
        break;
    case 2:
        Trace.TraceInformation("This is surely prime");
        break;
    default:
        break;
}
```


## Some Test Data
[Online Prime Numbers Generator and Checker](https://www.numberempire.com/primenumbers.php)    

[Table of Mersenne Prime](https://www.mersenne.org/primes/)    


Some Carmichael Number    
341,561,645,1105,1387,1389,1729,1905,2465,2821,6601,8911,    
10585,15841,29341,41041,46657,52633,62745,63973,75361,    
161038,215326,2568226,143742226

