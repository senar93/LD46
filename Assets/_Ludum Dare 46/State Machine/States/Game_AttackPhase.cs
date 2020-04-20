namespace LD46 {
    using Deirin.Utilities;
    using UnityEngine;

    public class Game_AttackPhase : GameStateBase {
        [Header("Event Listeners")]
        public GameEvent_Enemy OnEnemyDeath;
        public GameEvent OnEggDeath;

        private int enemiesFinishedAttacking;

        public override void Enter () {
            base.Enter();

            enemiesFinishedAttacking = 0;

            OnEggDeath.InvokeAction += EggDeathHandler;
            OnEnemyDeath.InvokeAction += EnemyDeathHandler;

            //all enemies attack
            foreach ( var attackingEnemy in context.currentLevel.enemies ) {
                attackingEnemy.OnAttackEnd.AddListener( EnemyAttackEndHandler );
                attackingEnemy.Attack();
            }
        }

        //when an enemy finishes his attack
        private void EnemyAttackEndHandler ( EnemyEntity enemy ) {
            enemiesFinishedAttacking++;
            //if all enemies finished attacking AND there are still enemies alive
            if ( enemiesFinishedAttacking >= context.currentLevel.enemies.Count ) {
                Unsubscribe();
                context.GoNext();
            }
        }

        //when an enemy dies
        private void EnemyDeathHandler ( EnemyEntity enemy ) {
            //if no more enemies => WIN
            if ( context.currentLevel.EnemiesLeft == false ) {
                Unsubscribe();
                context.GoWin();
            }
        }

        //quando l'uovo muore
        private void EggDeathHandler () {
            Unsubscribe();
            context.GoLoss();
        }

        private void Unsubscribe () {
            OnEggDeath.InvokeAction -= EggDeathHandler;
            OnEnemyDeath.InvokeAction -= EnemyDeathHandler;
            foreach ( var attackingEnemy in context.currentLevel.enemies ) {
                attackingEnemy.OnAttackEnd.RemoveListener( EnemyAttackEndHandler );
            }
        }
    }
}