namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using Sirenix.OdinInspector;
    using UnityEngine;

	public class EnemyEntity : BaseEntity
	{

		[Range(0, 255)]
		public int attackActionIndex = 0;
		[Range(0, 255)]
		public int movementActionIndex = 0;

		//[ReadOnly]
		public GridData currentGrid;
		[ReadOnly]
		public Vector2Int gridPosition = new Vector2Int(0, 0);
		[ReadOnly]
		public DirectionEnum enemyDirection = DirectionEnum.Up;


		public MoveActionEnum[] GetCurrentMovement()
		{
			MoveManager_Bh tmp;
			if(TryGetBehaviour(out tmp))
			{
				return tmp.CurrentAction.Movement;
			}

			return null;
		}

		/// <summary>
		/// Restituisce la lista delle celle che verranno attaccate da questo nemico
		/// </summary>
		/// <returns></returns>
		public Cell[] GetCurrentAttack()
		{
			return null;
		}

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

	}
}