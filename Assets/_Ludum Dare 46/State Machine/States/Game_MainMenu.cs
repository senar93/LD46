namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;
    using DG.Tweening;

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
            OnLevelButtonClick.InvokeAction += LevelButtonClickHandler;
        }

        private void LevelButtonClickHandler ( Level level ) {
            context.currentLevel = level;
            context.currentLevel.gameObject.SetActive( true );
            context.currentLevel.Setup();

            context.mainMenu.SetActive( false );

            ParticleSequence( level.Activate() );
            OnLevelActivationStart.Invoke();

            level.OnActivationSequenceEnd += LevelActivationHandler;
        }

        private void LevelActivationHandler () {
            OnLevelActivationEnd.Invoke();
            context.GoNext();
        }

        private void ParticleSequence ( float duration ) {
            context.ambientParticle.Play();
            Sequence s = DOTween.Sequence();
            s.Append( DOTween.To( GetSimSpeed, SetSimSpeed, 80, duration * .2f ) );
            s.AppendInterval( duration * .4f );
            s.Append( DOTween.To( GetSimSpeed, SetSimSpeed, 1, duration * .4f ).SetEase( Ease.OutQuint ) );
        }

        public override void Exit () {
            OnLevelButtonClick.InvokeAction -= LevelButtonClickHandler;
            context.currentLevel.OnActivationSequenceEnd -= LevelActivationHandler;
        }
    }
}