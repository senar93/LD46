namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;
    using DG.Tweening;
    using Deirin.Utilities;

    public class Game_MainMenu : GameStateBase {
        [Space, Header("Event Listeners")]
        public GameEvent_Level OnLevelButtonClick;

        [Header("Events")]
        public UnityEvent OnLevelActivationStart;
        public UnityEvent OnLevelActivationEnd;

        private float GetSimSpeed () => context.ambientParticle.main.simulationSpeed;
        private void SetSimSpeed ( float value ) {
            var main = context.ambientParticle.main;
            main.simulationSpeed = value;
        }

        public override void Enter () {
            base.Enter();

            context.mainMenu.gameObject.SetActive( true );

            context.currentLevel?.gameObject.SetActive( false );

            context.screenFader.blocksRaycasts = true;
            context.screenFader.blocksRaycasts = false;
            context.screenFader.DOFade( 0, 1 );

            OnLevelButtonClick.InvokeAction += LevelButtonClickHandler;
        }

        private void LevelButtonClickHandler ( Level level ) {
            context.currentLevel = level;
            context.currentLevel.gameObject.SetActive( true );
            context.currentLevel.Setup();

            context.mainMenu.DOFade( 0, .5f );

            level.Activate();
            ParticleSequence( true );
            OnLevelActivationStart.Invoke();

            level.OnActivationEnd.AddListener( LevelActivationHandler );
        }

        private void LevelActivationHandler () {
            ParticleSequence( false );
            OnLevelActivationEnd.Invoke();
            context.GoNext();
        }

        private void ParticleSequence ( bool value ) {
            if ( value ) {
                context.ambientParticle.Play();
                DOTween.To( GetSimSpeed, SetSimSpeed, 80, .4f );
            }
            else {
                DOTween.To( GetSimSpeed, SetSimSpeed, 1, .4f ).SetEase( Ease.OutQuint );
            }
        }

        public override void Exit () {
            base.Exit();

            OnLevelButtonClick.InvokeAction -= LevelButtonClickHandler;
            context.currentLevel.OnActivationEnd.RemoveListener( LevelActivationHandler );
        }
    }
}