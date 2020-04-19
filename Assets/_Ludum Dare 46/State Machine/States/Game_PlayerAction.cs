namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;
    using System;

    public class Game_PlayerAction : GameStateBase {
        [Header("Event Listeners")]
        public GameEvent OnPlayerActionBegin;
        public GameEvent OnPlayerActionEnd;

        public override void Enter () {
            base.Enter();

            if ( context.currentLevelTurnsLeft == 0 ) {
                context.GoWin();
                return;
            }

            context.player.moves = 1;
            context.movesCounter.UpdateUI( context.player.moves );

            OnPlayerActionBegin.InvokeAction += PlayerActionBeginHandler;
            OnPlayerActionEnd.InvokeAction += PlayerActionEndHandler;
        }

        private void PlayerActionBeginHandler () {
            context.player.moves--;
            context.movesCounter.UpdateUI( context.player.moves );
        }

        private void PlayerActionEndHandler () {
            if ( context.player.moves == 0 )
                context.GoNext();
        }

        public override void Exit () {
            base.Exit();

            OnPlayerActionEnd.InvokeAction -= PlayerActionEndHandler;
        }
    }
}