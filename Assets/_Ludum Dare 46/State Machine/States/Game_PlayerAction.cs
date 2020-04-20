namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

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
    }
}