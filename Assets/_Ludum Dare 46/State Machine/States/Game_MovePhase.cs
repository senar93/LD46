namespace LD46 {
    using System.Collections.Generic;
    using Deirin.Utilities;

    public class Game_MovePhase : GameStateBase {
        public GameEvent_Enemy OnEnemyDeath;
        public GameEvent OnEggDeath;

        private List<EnemyEntity> tempEnemies = new List<EnemyEntity>();

        public override void Enter () {
            base.Enter();

            OnEnemyDeath.InvokeAction += EnemyDeathHandler;
            OnEggDeath.InvokeAction += EggDeathHandler;

            //move all enemies
            foreach ( var enemy in context.currentLevel.enemies ) {
                enemy.OnMoveEnd.AddListener( EnemyMoveEndHandler );
                tempEnemies.Add( enemy );
                enemy.Move();
            }
        }

        private void EggDeathHandler () {
            context.GoLoss();
        }

        //each time an enemy finishes movement
        private void EnemyMoveEndHandler ( EnemyEntity enemy ) {
            if ( tempEnemies.Contains( enemy ) )
                tempEnemies.Remove( enemy );

            //if all enemies finished movement
            if ( tempEnemies.Count == 0 )
                context.GoNext();
        }

        private void EnemyDeathHandler ( EnemyEntity enemy ) {
            if ( context.currentLevel.EnemiesLeft == false )
                context.GoWin();
        }

        public override void Exit () {
            base.Exit();

            tempEnemies.Clear();

            OnEnemyDeath.InvokeAction -= EnemyDeathHandler;
            OnEggDeath.InvokeAction -= EggDeathHandler;
        }
    }
}