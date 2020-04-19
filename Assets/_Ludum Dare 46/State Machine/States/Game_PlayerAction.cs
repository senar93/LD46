namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

    public class Game_PlayerAction : GameStateBase {
        [Header("Event Listeners")]
        public GameEvent OnPlayerAction;

        public override void Enter () {
            base.Enter();

            if ( context.currentLevelTurnsLeft == 0 ) {
                context.GoWin();
                return;
            }

            OnPlayerAction.InvokeAction += PlayerActionHandler;
        }

        private void PlayerActionHandler () {
            context.player.Moves--;
            if ( context.player.Moves == 0 )
                context.GoNext();
        }

        public override void Exit () {
            base.Exit();

            OnPlayerAction.InvokeAction -= PlayerActionHandler;
        }
    }
}