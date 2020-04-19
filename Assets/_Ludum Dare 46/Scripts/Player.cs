namespace LD46 {
    using UnityEngine;
    using Deirin.Utilities;

    public class Player : MonoBehaviour {
        [SerializeField] private int moves;

        public UnityEvent_Int OnMovesChange;

        public int Moves {
            get => moves;
            set {
                if ( moves != value ) {
                    moves = value;
                    OnMovesChange.Invoke( moves );
                }
            }
        }
    }
}