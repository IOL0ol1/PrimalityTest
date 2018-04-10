namespace System.Numerics
{
    /// <summary>
    /// BigNumber Extension Class
    /// <para>see https://github.com/coapp-packages/mpir/blob/master/mpz/pprime_p.c </para>
    /// <para>see https://github.com/coapp-packages/mpir/blob/master/mpz/millerrabin.c </para> 
    /// </summary>
    public static class BigIntegerEx
    {

        /// <summary>
        /// Primality test
        /// </summary>
        /// <param name="n"></param>
        /// <param name="reps"> Miller-Rabin loop,25 passes are reasonable.</param>
        /// <param name="accuracy">accuracy</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"/>
        public static bool IsPrime(this BigInteger n, int reps, out double accuracy)
        {
            accuracy = 1;
            switch (IsPrime(n, reps))
            {
                case 0: /// number is not prime.
                    return false;
                case 1: /// number is 'probably' prime. probability is accuracy
                    accuracy = 1 - 1 / Math.Pow(4, reps);
                    return true;
                case 2: /// number is surely prime.
                    return true;
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// Primality test
        /// </summary>
        /// <param name="n">data</param>
        /// <param name="reps">Miller-Rabin loop,default 23</param>
        /// <returns>
        /// <para>0,n is not prime.</para>
        /// <para>1,n is 'probably' prime.</para>
        /// <para>2,n is surely prime.</para>
        /// </returns>
        /// <exception cref="ArgumentException"/>
        public static int IsPrime(BigInteger n, int reps = 23)
        {

            /// Handle small and negative number
            if (n < 0)
                throw new ArgumentException("please input a positive integer!");
            if (n < 1000000UL)
                return IsPrimeUInt64((UInt64)n) ? 2 : 0;

            /// If number is now even, it is not a prime. 
            if ((n & 1) == 0)
                return 0;

            /// Check if n has small factors.
            if (n % 3 == 0 || n % 5 == 0 || n % 7 == 0
                || n % 11 == 0 || n % 13 == 0 || n % 17 == 0
                || n % 19 == 0 || n % 23 == 0 || n % 29 == 0
                || n % 31 == 0 || n % 37 == 0 || n % 41 == 0
                || n % 43 == 0 || n % 47 == 0 || n % 53 == 0)
            {
                return 0;
            }

            /// Perform a number of Miller-Rabin tests.
            return MillerRabin(n, reps);
        }

        /// <summary>
        /// Primality test for number ∈[0, 1000000]
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static bool IsPrimeUInt64(UInt64 n)
        {
            UInt64 q, r, d;

            /// filter negative, even and 0,1,2
            if (n < 3 || (n & 1) == 0)
                return n == 2;

            for (d = 3, r = 1; r != 0; d += 2)
            {
                q = n / d;
                r = n - q * d;
                if (q < d)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// An implementation of the miller-rabin probabilistic primality test.
        /// </summary>
        /// <param name="n">input number</param>
        /// <param name="reps">loop count,default 23</param>
        /// <returns>
        /// <para>0,n is not prime.</para>
        /// <para>1,n is 'probably' prime.</para>
        /// </returns>
        private static int MillerRabin(BigInteger n, int reps)
        {
            BigInteger k, q, x, y;
            BigInteger nm1 = n - 1;

            /// Perform a Fermat test. 210 = 2*3*5*7, magic ???
            y = BigInteger.ModPow(210, nm1, n);
            if (y != 1) return 0; /// n is not prime.

            /// Find q and k, where q is odd and n - 1 = 2^k * q.
            for (k = 0, q = nm1; q.IsEven; k++, q >>= 1) ;

            int is_prime = 1;
            for (int r = 0; (r < reps) && (is_prime > 0); r++)
            {
                x = GenerateRandomBigInteger(1, nm1);
                is_prime = MillerRabinInner(n, x, q, k);
            }
            return is_prime;
        }

        private static int MillerRabinInner(BigInteger n, BigInteger x, BigInteger q, BigInteger k)
        {
            BigInteger nm1 = n - 1;

            BigInteger y = BigInteger.ModPow(x, q, n);

            if (y == 1 || y == nm1)
                return 1;

            for (BigInteger i = 1; i < k; i++)
            {
                y = BigInteger.ModPow(y, 2, n);
                if (y == nm1)
                    return 1;
                if (y == 1)
                    return 0;
            }
            return 0;
        }

        /// <summary>
        ///  Generate a random bigInteger ∈ [min,max]
        /// <para>see https://github.com/mikolajtr/dotnetprime/blob/master/MillerRabin/Helpers/PrimeGeneratorHelpers.cs#L8 </para>
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>A random bigInteger</returns>
        public static BigInteger GenerateRandomBigInteger(BigInteger min, BigInteger max)
        {
            Random random = new Random();
            byte[] bytes = max.ToByteArray();

            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F; /// force sign bit to positive
            BigInteger value = new BigInteger(bytes);
            BigInteger result = (value % (max - min + 1)) + min;

            return result;
        }
    }
}
