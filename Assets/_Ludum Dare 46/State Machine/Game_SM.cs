namespace LD46 {
    using Deirin.StateMachine;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Game_SM : StateMachineBase {
        public GameContext context;

        protected override void CustomDataSetup () {
            context.Next += Next;
            context.Win += Win;
            context.Loss += Loss;
            data = context;
        }

        private void Next () {
            animator.SetTrigger( "Next" );
        }

        private void Win () {
            animator.SetTrigger( "Win" );
        }

        private void Loss () {
            animator.SetTrigger( "Loss" );
        }
    }

    [Serializable]
    public class GameContext : IStateMachineData {
        [Header("References")]
        public List<Level> levels;
        public Transform levelButtonsContainer;
        public ParticleSystem ambientParticle;
        public Player player;
        public GameObject mainMenu;
        public GridData gridData;
        public CounterUI turnCounter, movesCounter;

        [HideInInspector] public Level currentLevel;
        [HideInInspector] public int currentLevelTurnsLeft;
        public Action Next, Win, Loss;

        public void GoNext () {
            Next.Invoke();
        }

        public void GoWin () {
            Win.Invoke();
        }

        public void GoLoss () {
            Loss.Invoke();
        }
    }
}