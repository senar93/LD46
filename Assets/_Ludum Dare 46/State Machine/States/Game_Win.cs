namespace LD46 {
    using Deirin.Utilities;
    using DG.Tweening;
    using UnityEngine.SceneManagement;

    public class Game_Win : GameStateBase {
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
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }

        public override void Exit () {
            OnLevelEndButtonClick.InvokeAction -= LevelEndButtonClickHandler;
        }
    }
}