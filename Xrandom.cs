using System;
using System.Security.Cryptography;

namespace run_runner
{
	public static class XRandom
	{
		public static Random? random;
		private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

		public static void Init()
		{
			random = new Random(DateTime.Now.Millisecond);
		}

		public static int Next(int n)
		{
			return Between(0, n);
		}

		public static float NextFloat(float max)
		{
			random = new Random(DateTime.Now.Millisecond);
			float randomFloat = (float) random.NextDouble() % max;
			return randomFloat;
		}

		public static int Next(int n, int m)
		{
			return Between(Math.Min(n, m), Math.Max(n, m));
		}

		public static int Between(int minimumValue, int maximumValue)
		{
			byte[] randomNumber = new byte[1];

			_generator.GetBytes(randomNumber);

			double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

			// We are using Math.Max, and substracting 0.00000000001,
			// to ensure "multiplier" will always be between 0.0 and .99999999999
			// Otherwise, it's possible for it to be "1", which causes problems in our rounding.
			double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

			// We need to add one to the range, to allow for the rounding done with Math.Floor
			int range = maximumValue - minimumValue + 1;

			double randomValueInRange = Math.Floor(multiplier * range);

			return (int) (minimumValue + randomValueInRange);
		}

		public static string RandomString()
		{
			// create a stronger hash code using RNGCryptoServiceProvider
			byte[] randomLocal = new byte[64];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			// populate with random bytes
			rng.GetBytes(randomLocal);

			// convert random bytes to string
			string randomBase64 = Convert.ToBase64String(randomLocal);
			// display
			return randomBase64;
		}
	}
}