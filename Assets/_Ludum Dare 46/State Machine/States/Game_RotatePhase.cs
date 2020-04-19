namespace LD46 {
    using System.Collections.Generic;

    public class Game_RotatePhase : GameStateBase {
        private List<EnemyEntity> tempEnemies = new List<EnemyEntity>();

        public override void Enter () {
            base.Enter();

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
            context.turnCounter.UpdateUI( context.currentLevelTurnsLeft );
        }
    }
}