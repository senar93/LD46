namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class AbsCondition : BaseBehaviour
	{
		public abstract bool Check();
	}
}