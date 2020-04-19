namespace LD46 {
    using Deirin.StateMachine;
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class GameStateBase : StateBase {
        [Header("State Events")]
        public UnityEvent OnEnter;
        public UnityEvent OnExit;

        protected GameContext context;

        protected override void CustomSetup () {
            context = data as GameContext;
        }

        public override void Enter () {
            OnEnter.Invoke();
        }

        public override void Exit () {
            OnExit.Invoke();
        }
    }
}