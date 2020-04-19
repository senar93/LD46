namespace LD46 {
    using System.Collections.Generic;

    public class Game_AttackPhase : GameStateBase {
        private List<EnemyEntity> tempEnemies = new List<EnemyEntity>();

        public override void Enter () {
            base.Enter();

            //all enemies attack
            foreach ( var attackingEnemy in context.currentLevel.enemies )
                foreach ( var cell in attackingEnemy.GetAttackTargets() )
                    //check hit
                    foreach ( var possibleTargetedEnemy in context.currentLevel.enemies ) {

                        //save hit enemies
                        if ( cell == possibleTargetedEnemy.cell && tempEnemies.Contains( possibleTargetedEnemy ) == false )
                            tempEnemies.Add( possibleTargetedEnemy );

                        //check egg hit => if so LOSS
                        if ( cell == context.currentLevel.egg.cell ) {
                            context.currentLevel.egg.Die();
                            context.GoLoss();
                            return;
                        }
                    }

            //kill all hit enemies
            foreach ( var enemy in tempEnemies ) {
                enemy.Die();
            }

            tempEnemies.Clear();

            //if no more enemies => go WIN
            if ( context.currentLevel.EnemiesLeft == false ) {
                context.GoWin();
                return;
            }

            context.GoNext();
        }

        public override void Exit () {
            base.Exit();

            tempEnemies.Clear();
        }
    }
}