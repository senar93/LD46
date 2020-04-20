namespace LD46.Conditions
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using SenarCustomSystem.Utility;
	using static SenarCustomSystem.Utility.ComparisonTypeUtility;

	[AddComponentMenu("_Ludum Dare 46/Enemy/Conditions/Rotation Index")]
	public class RotationIndex : AbsCondition
	{
		[Title("Condition Parameters"), SerializeField]
		private NumberComparisonType comparrisonType;
		[SerializeField]
		private int indexToCompare;

		public override bool Check()
		{
			EnemyEntity enemyEntity = Entity as EnemyEntity;

			return comparrisonType.Compare(enemyEntity.rotationActionIndex, indexToCompare);
		}
	}
}