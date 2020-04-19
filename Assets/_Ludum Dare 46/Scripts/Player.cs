namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

    public class Player : MonoBehaviour {
        [ReadOnly] public int moves;

        public UnityEvent_Int OnMovesChange;
    }
}