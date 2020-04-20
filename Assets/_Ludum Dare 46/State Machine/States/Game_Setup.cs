namespace LD46 {
    using UnityEngine;

    public class Game_Setup : GameStateBase {
        [Header("Prefabs")]
        public LevelButton levelButtonPrefab;

        public override void Enter () {
            base.Enter();

            foreach ( var level in context.levels ) {
                level.gameObject.SetActive( false );
                level.gridData = context.gridData;
                LevelButton lb = Instantiate( levelButtonPrefab, context.levelButtonsContainer );
                lb.Setup( level );
            }

            context.GoNext();
        }
    }
}