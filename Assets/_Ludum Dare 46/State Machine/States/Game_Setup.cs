namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

    public class Game_Setup : GameStateBase {
        [Header("Prefabs")]
        public LevelButton levelButtonPrefab;

        [Space, Header("Events")]
        public GameEvent OnScreenFadeIn;

        public override void Enter () {
            base.Enter();

            foreach ( var level in context.levels ) {
                level.gameObject.SetActive( false );
                level.gridData = context.gridData;
                LevelButton lb = Instantiate( levelButtonPrefab, context.levelButtonsContainer );
                lb.Setup( level );
            }

            OnScreenFadeIn.InvokeAction += ScreenFadeInHandler;
        }

        private void ScreenFadeInHandler () {
            context.GoNext();
        }

        public override void Exit () {
            base.Exit();
            OnScreenFadeIn.InvokeAction -= ScreenFadeInHandler;
        }
    }
}