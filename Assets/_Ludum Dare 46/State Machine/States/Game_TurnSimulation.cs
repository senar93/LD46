namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

    public class Game_TurnSimulation : GameStateBase {
        [Header("Event Listeners")]
        public GameEvent OnEggDeath;
        public GameEvent_Enemy OnEnemyDeath;

        public override void Enter () {
            base.Enter();

            OnEggDeath.InvokeAction += EggDeathHandler;
            OnEnemyDeath.InvokeAction += EnemyDeathHandler;
        }

        private void EnemyDeathHandler ( EnemyEntity enemy ) {
            context.currentLevel.enemies.Remove( enemy );
            if ( context.currentLevel.EnemiesLeft == false )
                context.GoWin();
        }

        private void EggDeathHandler () {
            context.GoLoss();
        }

        public override void Exit () {
            base.Exit();

            context.currentLevelTurnsLeft--;

            OnEggDeath.InvokeAction -= EggDeathHandler;
            OnEnemyDeath.InvokeAction -= EnemyDeathHandler;
        }
    }
}