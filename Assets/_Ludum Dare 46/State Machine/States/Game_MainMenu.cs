namespace LD46 {
    using UnityEngine;

    public class Game_MainMenu : GameStateBase {
        [Space, Header("Events")]
        public GameEvent_Level OnLevelButtonClick;

        public override void Enter () {
            OnLevelButtonClick.InvokeAction += LevelButtonClickHandler;
        }

        private void LevelButtonClickHandler ( Level level ) {
            level.Activate();
        }

        public override void Exit () {
            OnLevelButtonClick.InvokeAction -= LevelButtonClickHandler;
        }
    }
}