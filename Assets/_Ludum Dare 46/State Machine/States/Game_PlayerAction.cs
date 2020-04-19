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

            context.player.moves = 1;

            OnPlayerAction.InvokeAction += PlayerActionHandler;
        }

        //public override void Tick () {
        //    if ( Input.GetKeyDown( KeyCode.Space ) )
        //        OnPlayerAction.Invoke();
        //}

        private void PlayerActionHandler () {
            context.player.moves--;
            if ( context.player.moves == 0 )
                context.GoNext();
        }

        public override void Exit () {
            base.Exit();

            OnPlayerAction.InvokeAction -= PlayerActionHandler;
        }
    }
}