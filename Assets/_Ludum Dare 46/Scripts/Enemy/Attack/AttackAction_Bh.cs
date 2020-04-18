namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;

	public class AttackAction_Bh : AbsAction_Bh
	{
		//pattern attacco da effettuare
		[Title("Parameters"), Range(1,10), SerializeField, HideIf("attackPattern")]
		int radius;

		[Title("Parameters"), SerializeField, ShowIf("attackPattern")]
		private bool[,] attackPattern;


		[Title("Next Attack Index"), Range(0, 255), SerializeField, Space]
		private int nextAttackIndexValue = 0;
		private EnemyStatus_Bh enemyStatus;
		public void SetNextAttackIndex()
		{
			if (enemyStatus || Entity.TryGetBehaviour<EnemyStatus_Bh>(out enemyStatus))
			{
				enemyStatus.attackActionIndex = nextAttackIndexValue;
			}
		}

		#region EDITOR
		[Button("Set Attack Pattern", ButtonSizes.Large), HideIf("attackPattern"), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void SetAttackPattern()
		{
			attackPattern = new bool[radius * 2 + 1, radius * 2 + 1];
		}

		[Button("Destory Attack Pattern", ButtonSizes.Medium), ShowIf("attackPattern"), GUIColor(1f, 0.5f, 0.5f, 1f)]
		private void DestroyAttackPattern()
		{
			attackPattern = null;
		}

		#endregion
	}
}