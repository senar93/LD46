namespace LD46 {
    public class Game_Level : GameStateBase {
        public override void Enter () {
            base.Enter();

            context.currentLevelTurnsLeft = context.currentLevel.turns;

            context.turnCounter.UpdateUI( context.currentLevelTurnsLeft );

            //context.turnCounter.horizontalLayoutGroup.enabled = false;
            //context.movesCounter.horizontalLayoutGroup.enabled = false;

            context.GoNext();
        }
    }
}