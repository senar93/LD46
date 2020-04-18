//TODO: da implementare meglio e in modo decente, però l'idea non è male
namespace SenarCustomSystem.Utility
{
	using System.Collections.Generic;

	public static class ComparisonTypeUtility
	{
		public static bool Compare(this NumberComparisonType comparison, float value1, float value2)
		{
			switch (comparison)
			{
				case NumberComparisonType.Equal:
					return value1 == value2;
				case NumberComparisonType.Greater:
					return value1 > value2;
				case NumberComparisonType.GreaterOrEqual:
					return value1 >= value2;
				case NumberComparisonType.Lesser:
					return value1 < value2;
				case NumberComparisonType.LesserOrEqual:
					return value1 <= value2;
				case NumberComparisonType.NotEqual:
					return value1 != value2;
			}

			return false;
		}

		public static bool Compare<T>(this EqualityComparisonType comparison, T value1, T value2)
		{
			if (comparison == EqualityComparisonType.Equal)
			{
				return EqualityComparer<T>.Default.Equals(value1, value2);
			}
			else
			{
				return !EqualityComparer<T>.Default.Equals(value1, value2);
			}
		}

		/// <summary>
		/// permette di comparare valori che possono essere:  > , < , >= , <= , == , !=
		/// </summary>
		public enum NumberComparisonType
		{
			Equal = 0,
			Greater = 1,
			GreaterOrEqual = 2,
			Lesser = 3,
			LesserOrEqual = 4,
			NotEqual = 5
		}

		public enum EqualityComparisonType
		{
			Equal = 0,
			NotEqual = 1
		}

	}

}