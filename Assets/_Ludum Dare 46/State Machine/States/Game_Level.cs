﻿namespace LD46 {
    public class Game_Level : GameStateBase {
        public override void Enter () {
            base.Enter();
            context.GoNext();
        }
    }
}