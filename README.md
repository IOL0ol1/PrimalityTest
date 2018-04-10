# PrimalityTest

primality test (Miller Rabin) implement by C#,     
transplantation from a C Language Library [Mpir](https://github.com/coapp-packages/mpir)    


# Features

1. use System.Numerics.BigInteger (NET4 and later) support any size integer (Memory allowable).      
2. for input < 1000000, use determinate algorithm, The result is accurate.    

# How to

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