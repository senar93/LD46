namespace LD46 {
    using Deirin.StateMachine;
    using System;
    using UnityEngine;

    public class Game_SM : StateMachineBase {
        public GameContext context;

        protected override void CustomDataSetup () {
            context.Next += Next;
            data = context;
        }

        private void Next () {
            animator.SetTrigger( "Next" );
        }
    }

    [Serializable]
    public class GameContext : IStateMachineData {
        [Header("References")]
        public Level[] levels;
        [HideInInspector] public Level currentLevel;
        public Transform levelButtonsContainer;
        public ParticleSystem ambientParticle;
        public Player player;

        public Action Next;

        public void GoNext () {
            Next.Invoke();
        }
    }
}