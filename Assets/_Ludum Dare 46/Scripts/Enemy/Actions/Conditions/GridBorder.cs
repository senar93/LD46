namespace LD46.Conditions
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using SenarCustomSystem.Utility;
	using static SenarCustomSystem.Utility.ComparisonTypeUtility;

	[AddComponentMenu("_Ludum Dare 46/Enemy/Conditions/Grid Border")]
	public class GridBorder : AbsCondition
	{
		[SerializeField]
		private DirectionEnum gridBorder;

		public override bool Check()
		{
			EnemyEntity enemyEntity = Entity as EnemyEntity;

			switch (gridBorder)
			{
				case DirectionEnum.Down:
					return enemyEntity.cell.y == 0;

				case DirectionEnum.Right:
					return enemyEntity.cell.y == enemyEntity.currentGrid.cells.GetLength(0);

				case DirectionEnum.Up:
					return enemyEntity.cell.y == enemyEntity.currentGrid.cells.GetLength(1);

				case DirectionEnum.Left:
					return enemyEntity.cell.x == 0;

			}

			return false;
		}
	}
}