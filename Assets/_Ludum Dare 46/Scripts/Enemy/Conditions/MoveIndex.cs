namespace LD46.Conditions
{
	using Deirin.EB;
    using Sirenix.OdinInspector;
    using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using SenarCustomSystem.Utility;
    using static SenarCustomSystem.Utility.ComparisonTypeUtility;

    public class MoveIndex : AbsCondition
	{
		[Title("Condition Parameters"), SerializeField]
		private NumberComparisonType comparrisonType;
		[SerializeField]
		private int indexToCompare;

		public override bool Check()
		{
			EnemyEntity enemyEntity = Entity as EnemyEntity;

			return comparrisonType.Compare(enemyEntity.movementActionIndex, indexToCompare); 
		}
	}
}