namespace LD46.Conditions
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using SenarCustomSystem.Utility;
	using static SenarCustomSystem.Utility.ComparisonTypeUtility;

	[AddComponentMenu("_Ludum Dare 46/Enemy/Conditions/Point Outside Grid")]
	public class PointOutsideGrid : AbsCondition
	{
		public override bool Check()
		{
			EnemyEntity enemyEntity = Entity as EnemyEntity;

			switch (enemyEntity.enemyDirection)
			{
				case DirectionEnum.Down:
					if( enemyEntity.cell.y == 0)
					{
						return true;
					}
					break;
				case DirectionEnum.Right:
					if(enemyEntity.cell.y == enemyEntity.currentGrid.cells.GetLength(0))
					{
						return true;
					}
					break;
				case DirectionEnum.Up:
					if( enemyEntity.cell.y == enemyEntity.currentGrid.cells.GetLength(1))
					{
						return true;
					}
					break;
				case DirectionEnum.Left:
					if( enemyEntity.cell.x == 0)
					{
						return true;
					}
					break;
			}

			return false;
		}
	}
}