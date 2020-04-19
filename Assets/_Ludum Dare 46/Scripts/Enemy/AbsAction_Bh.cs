namespace LD46
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System.Linq;

	public abstract class AbsAction_Bh : BaseBehaviour
	{
		[Title("Action Parameters"), Range(0, 255)]
		public int index = 0;

		[Title("Conditions"), GUIColor(1f, 1f, 0.9f, 1f), ReadOnly, Space]
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

		public abstract void SetNextIndex();

		protected override void CustomSetup()
		{
			base.CustomSetup();
			conditions = GetComponentsInChildren<AbsCondition>().ToList<AbsCondition>();
		}

		protected bool ValidateGridCoords(int x, int y, Vector2Int gridSize)
		{
			return x >= 0 &&
				   y >= 0 &&
				   x < gridSize.x &&
				   y < gridSize.y;
		}
	}
}