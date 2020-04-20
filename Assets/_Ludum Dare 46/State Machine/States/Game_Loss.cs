namespace LD46 {
    using Deirin.Utilities;
    using DG.Tweening;

    public class Game_Loss : GameStateBase {
        public GameEvent OnLevelEndButtonClick;

        public override void Enter () {
            base.Enter();

            OnLevelEndButtonClick.InvokeAction += LevelEndButtonClickHandler;
        }

        private void LevelEndButtonClickHandler () {
            context.screenFader.blocksRaycasts = true;
            context.screenFader.DOFade( 1, .8f ).onComplete += FadeOutHandler;
        }

        private void FadeOutHandler () {
            context.screenFader.blocksRaycasts = false;
            context.mainMenu.alpha = 1;
            context.GoNext();
        }

        public override void Exit () {
            OnLevelEndButtonClick.InvokeAction -= LevelEndButtonClickHandler;
        }
    }
}