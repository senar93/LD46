namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;

	public abstract class AbsMananger_Bh<T> : BaseBehaviour where T : AbsAction_Bh 
	{
		[ShowInInspector, ReadOnly]
		private T[] _actions = null;

		public T[] Actions {
			get {
				if (_actions == null && _actions.Length == 0)
				{
					PopulateActions();
				}

				return _actions;
			}
		}

		public T CurrentAction {
			get {
				foreach (T item in Actions)
				{
					if (item.ConditionsFlag)
					{
						return item;
					}
				}

				return null;
			}
		}

		protected override void CustomSetup()
		{
			_actions = null;
			base.CustomSetup();
			PopulateActions();
		}

		public void GoToNextAction()
		{
			CurrentAction.SetNextIndex();
		}

		public void PopulateActions()
		{
			_actions = GetComponentsInChildren<T>();
		}

	}
}