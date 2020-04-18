namespace LD46
{
	using Deirin.EB;
    using Sirenix.OdinInspector;
    using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class MoveAction_Bh : AbsAction_Bh
	{
		[Title("Movement"), Required, EnumToggleButtons]
		private List<MoveActionEnum> _movement = new List<MoveActionEnum>();

		public MoveActionEnum[] Movement 
		{
			get => _movement.ToArray();
		}


		[Title("Next Movement Index"), Range(0, 255), SerializeField, Space]
		private int nextMovementIndexValue = 0;
		private EnemyStatus_Bh enemyStatus;
		public override void SetNextIndex()
		{		
			if(enemyStatus || Entity.TryGetBehaviour<EnemyStatus_Bh>(out enemyStatus))
			{
				enemyStatus.movementActionIndex = nextMovementIndexValue;
			}
		}
	}

	public enum MoveActionEnum 
	{
		GoForward = 1,
		TurnLeft = 2,
		TurnRight = 3
	}
}