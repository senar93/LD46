﻿namespace LD46 {
    using System.Collections.Generic;

    public class Game_TurnSimulation : GameStateBase {
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

            //move all enemies
            foreach ( var enemy in context.currentLevel.enemies ) {
                enemy.OnMoveEnd.AddListener( EnemyMoveEndHandler );
                tempEnemies.Add( enemy );
                enemy.Move();
            }
        }

        //each time an enemy finishes movement
        private void EnemyMoveEndHandler ( EnemyEntity enemy ) {
            if ( tempEnemies.Contains( enemy ) )
                tempEnemies.Remove( enemy );

            //if all enemies finished movement
            if ( tempEnemies.Count == 0 )
                EnemiesRotationPhase();
        }

        private void EnemiesRotationPhase () {
            foreach ( var rotatingEnemy in context.currentLevel.enemies ) {
                rotatingEnemy.OnRotateEnd.AddListener( EnemyRotateEndHandler );
                tempEnemies.Add( rotatingEnemy );
                rotatingEnemy.Rotate();
            }
        }

        //each time an enemy finishes rotation
        private void EnemyRotateEndHandler ( EnemyEntity enemy ) {
            if ( tempEnemies.Contains( enemy ) )
                tempEnemies.Remove( enemy );

            //if all enemies are done rotating => we go NEXT
            if ( tempEnemies.Count == 0 )
                context.GoNext();
        }

        public override void Exit () {
            base.Exit();

            tempEnemies.Clear();

            context.currentLevelTurnsLeft--;
        }
    }
}