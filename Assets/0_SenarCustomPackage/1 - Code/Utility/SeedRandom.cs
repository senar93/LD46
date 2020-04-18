namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;

	[System.Serializable]
	public class SeedRandom
	{
		[InlineButton("DontUseSeed", "Don't use Seed"), ShowIf("useSeed"), PropertyOrder(int.MinValue)]
		public int seed;

		[MaxValue("max"), Space] public int min;
		[MinValue("min")] public int max;

		[ReadOnly, ShowInInspector, HideInEditorMode] public int currentValue { get; protected set; }
		[ReadOnly, ShowInInspector, HideInEditorMode] public int index { get; protected set; }

		protected System.Random rnd;
		private bool useSeed = false;
		protected int oldSeed;

		public SeedRandom(int seed, int min, int max)
		{
			System.Random tmpRnd = new System.Random();
			this.seed = useSeed ? seed : tmpRnd.Next(int.MinValue, int.MaxValue);
			this.min = min;
			this.max = max;
			this.index = -1;
			this.rnd = new System.Random(seed);
			this.currentValue = GetNextValue();
		}

		public int GetNextValue()
		{
			index++;
			currentValue = rnd.Next(min, max);
			return currentValue;
		}




		[Button("Use Seed"), HideIf("useSeed"), PropertyOrder(int.MinValue)]
		public void UseSeed()
		{
			useSeed = true;
		}

		public void DontUseSeed()
		{
			useSeed = false;

		}

	}
}