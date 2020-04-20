namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;
	using System.Linq;
    using System.Collections.Generic;

    public class Game_PlayerAction : GameStateBase {
        [Header("Event Listeners")]
        public GameEvent OnPlayerActionBegin;
        public GameEvent OnPlayerActionEnd;
        public GameEvent OnActionSkipButtonClick;

        public override void Enter () {
            base.Enter();

            if ( context.currentLevelTurnsLeft == 0 ) {
                Unsubscribe();
                context.GoWin();
                return;
            }

            context.player.moves = 1;
            context.skipActionButton.Active( true );

            OnPlayerActionBegin.InvokeAction += PlayerActionBeginHandler;
            OnPlayerActionEnd.InvokeAction += PlayerActionEndHandler;
            OnActionSkipButtonClick.InvokeAction += ActionSkipButtonClickHandler;

			//check egg danger thing => ondanger;
			if (CheckEggDanger())
				context.currentLevel.egg.SetEggDanger();
			else
				context.currentLevel.egg.SetEggSafe();

		}

        private void ActionSkipButtonClickHandler () {
            Unsubscribe();
            context.GoNext();
        }

        private void PlayerActionBeginHandler () {
            context.player.moves--;
        }

        private void PlayerActionEndHandler () {
            if ( context.player.moves == 0 ) {
                Unsubscribe();
                context.GoNext();
            }
        }

        private void Unsubscribe () {
            OnPlayerActionBegin.InvokeAction -= PlayerActionBeginHandler;
            OnPlayerActionEnd.InvokeAction -= PlayerActionEndHandler;
            OnActionSkipButtonClick.InvokeAction -= ActionSkipButtonClickHandler;
        }

        public override void Exit () {
            base.Exit();

            context.skipActionButton.Active( false );
        }

		public bool CheckEggDanger() 
		{
			List<Cell> hittedCell = new List<Cell>();
			List<EnemyEntity> tmpEnemy = new List<EnemyEntity>(context.currentLevel.enemies);
			
			hittedCell = context.currentLevel.GetAllThreatenedCell();

			//remove enemy "killed"
			foreach (EnemyEntity enemy in context.currentLevel.enemies)
			{
				if(hittedCell.Contains(enemy.cell))
				{
					tmpEnemy.Remove(enemy);
				}
			}

			//check alive enemy movement
			foreach(EnemyEntity enemy in tmpEnemy)
			{
				hittedCell.AddRange(enemy.GetMovementCells());
			}

			hittedCell = hittedCell.Distinct().ToList();

			return hittedCell.Contains(context.currentLevel.egg.cell);
		}
    }
}