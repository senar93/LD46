namespace LD46
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class AbsAction_Bh : BaseBehaviour
	{
		[Title("Action Parameters"), Range(0, 255)]
		public int index = 0;

		[Title("Conditions"), Required, Space]
		public List<AbsCondition> conditions = new List<AbsCondition>();


		[ReadOnly, ShowInInspector]
		public bool ConditionsFlag {
			get {
				if (conditions != null)
				{
					for (int i = 0; i < conditions.Count; i++)
					{
						bool tmp = (conditions[i] != null) ? conditions[i].Check() : true;
						if (tmp != true)
						{
							return false;
						}
					}
				}

				return true;    //non ci sono condizioni
			}
		}

	}
}