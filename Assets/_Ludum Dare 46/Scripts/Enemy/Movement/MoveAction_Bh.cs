namespace LD46
{
	using Deirin.EB;
    using Sirenix.OdinInspector;
    using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class MoveAction_Bh : AbsAction_Bh
	{
		[Title("Movement"), GUIColor(0.9f, 0.9f, 1f, 1f), Range(0, 10), SerializeField, Required, EnumToggleButtons]
		public int movement = 1;

		/// <summary>
		/// WIP
		/// </summary>
		public Cell[] SelectedCells {
			get {
				EnemyEntity enemyEntity = (Entity as EnemyEntity);
				//fetch grid size
				Vector2Int gridSize = new Vector2Int();
				gridSize.x = enemyEntity.currentGrid.cells.GetLength(0);
				gridSize.y = enemyEntity.currentGrid.cells.GetLength(1);
				//fetch current position
				Vector2Int cp = new Vector2Int();
				cp.Set(enemyEntity.cell.x, enemyEntity.cell.y);
				//Debug.LogError(cp);

				List<Cell> tmpList = new List<Cell>();

				int x = 0;
				int y = 0;
				for (int i = 1; i <= movement; i++)
				{
					switch (enemyEntity.enemyDirection)
					{
						case DirectionEnum.Down:
							x = 0;
							y = -i;
							break;
						case DirectionEnum.Right:
							x = i;
							y = 0;
							break;
						case DirectionEnum.Up:
							x = 0;
							y = i;
							break;
						case DirectionEnum.Left:
							x = -i;
							y = 0;
							break;
					}

					if (ValidateGridCoords(cp.x + x, cp.y + y, gridSize))
					{
						tmpList.Add(enemyEntity.currentGrid.cells[cp.x + x, cp.y + y]);
					}
					else 
					{
						break;
					}
				}

				return tmpList.ToArray();
			}
		}


		[Title("Next Movement Index"), Range(0, 255), SerializeField, Space]
		private int nextMovementIndexValue = 0;
		public override void SetNextIndex()
		{		
			((EnemyEntity) Entity).movementActionIndex = nextMovementIndexValue;
		}
	}
}