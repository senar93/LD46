namespace LD46
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using UnityEngine;

	public class EnemyEntity : BaseEntity
	{

		[Range(0, 255)]
		public int attackActionIndex = 0;
		[Range(0, 255)]
		public int movementActionIndex = 0;
		[Range(0, 255)]
		public int rotationActionIndex = 0;

		public GridData currentGrid;
		public Cell cell;
		public DirectionEnum enemyDirection = DirectionEnum.Down;


		#region API GET SOMETHING
		/// <summary>
		/// la lista delle celle in cui si muoverà l'entity, ordinate
		/// </summary>
		/// <returns></returns>
		public Cell[] GetMovementCells()
		{
			MoveManager_Bh tmp;
			if(TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.SelectedCells;
			}

			return null;
		}

		/// <summary>
		/// l'ultima cella in cui si muoverà l'entity
		/// </summary>
		/// <returns></returns>
		public Cell GetMovementLastCell()
		{
			MoveManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.SelectedCells[tmp.CurrentAction.SelectedCells.Length - 1];
			}

			return null;
		}

		/// <summary>
		/// Restituisce la lista delle celle che verranno attaccate da questo nemico
		/// </summary>
		/// <returns></returns>
		public Cell[] GetAttackTargets()
		{
			AttackManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.SelectedCells;
			}

			return null;
		}

		/// <summary>
		/// da che lato sta ruotando entity
		/// </summary>
		/// <returns></returns>
		public RotateActionEnum GetRotationAction()
		{
			RotationManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.rotation;
			}

			return RotateActionEnum.none;
		}

		/// <summary>
		/// la vera rotazione di entity
		/// </summary>
		/// <returns></returns>
		public DirectionEnum GetNewGlobalDirection()
		{
			RotationManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.NewEntityDirection;
			}

			return enemyDirection;
		}

		#endregion

		#region API INDEX UPDATE
		public void GoToNextMovementAction()
		{
			MoveManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				tmp.GoToNextAction();
			}
		}

		public void GoToNextAttackAction()
		{
			AttackManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				tmp.GoToNextAction();
			}
		}

		public void GoToNextRotationAction()
		{
			RotationManager_Bh tmp;
			if (TryGetBehaviour(out tmp))
			{
				tmp.GoToNextAction();
			}
		}

		#endregion


		[Button("TEST!", ButtonSizes.Large), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void SetAttackPattern()
		{
			Cell[] cells = GetAttackTargets();//GetCurrentMovementCells();
#if UNITY_EDITOR
			Debug.Log( "Player Position: " + cell.x + "," + cell.y );
			Debug.Log( "Lenght: " + cells.Length );
			for ( int i = 0; i < cells.Length; i++ )
				Debug.Log( "Cells[ " + cells[i].x + " , " + cells[i].y + " ]" ); 
#endif
		}

	}
}