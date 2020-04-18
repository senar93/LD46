namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EnemyEntity : BaseEntity
	{
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