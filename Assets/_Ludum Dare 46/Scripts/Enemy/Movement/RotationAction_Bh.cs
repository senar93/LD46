namespace LD46
{
	using Deirin.EB;
	using Sirenix.OdinInspector;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class RotationAction_Bh : AbsAction_Bh
	{
		[Title("Movement"), GUIColor(0.9f, 0.9f, 1f, 1f), SerializeField, Required, EnumToggleButtons]
		public RotateActionEnum rotation;

		/// <summary>
		/// WIP, passa direttamente la nuova rotazione del entity, in base alla rotazione attuale
		/// </summary>
		public DirectionEnum NewEntityDirection {
			get {
				switch ((Entity as EnemyEntity).enemyDirection)
				{
					case DirectionEnum.Down:
						switch (rotation)
						{
							case RotateActionEnum.TurnLeft:
								return DirectionEnum.Right;
							case RotateActionEnum.TurnRight:
								return DirectionEnum.Left;
							case RotateActionEnum.Turn180:
								return DirectionEnum.Up;
						}
						break;
					case DirectionEnum.Right:
						switch (rotation)
						{
							case RotateActionEnum.TurnLeft:
								return DirectionEnum.Up;
							case RotateActionEnum.TurnRight:
								return DirectionEnum.Down;
							case RotateActionEnum.Turn180:
								return DirectionEnum.Left;
						}
						break;
					case DirectionEnum.Up:
						switch (rotation)
						{
							case RotateActionEnum.TurnLeft:
								return DirectionEnum.Left;
							case RotateActionEnum.TurnRight:
								return DirectionEnum.Right;
							case RotateActionEnum.Turn180:
								return DirectionEnum.Down;
						}
						break;
					case DirectionEnum.Left:
						switch (rotation)
						{
							case RotateActionEnum.TurnLeft:
								return DirectionEnum.Down;
							case RotateActionEnum.TurnRight:
								return DirectionEnum.Up;
							case RotateActionEnum.Turn180:
								return DirectionEnum.Right;
						}
						break;
				}

				return (Entity as EnemyEntity).enemyDirection;
			}
		}

		[Title("Next Movement Index"), Range(0, 255), SerializeField, Space]
		private int nextRotationIndexValue = 0;
		public override void SetNextIndex()
		{
			((EnemyEntity)Entity).rotationActionIndex = nextRotationIndexValue;
		}
	}

	public enum RotateActionEnum
	{
		TurnLeft = 1,
		TurnRight = 2,
		Turn180 = 3,
		none = 0
	}
}