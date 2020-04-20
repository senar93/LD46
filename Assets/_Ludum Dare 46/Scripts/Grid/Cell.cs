namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;
    using Sirenix.OdinInspector;

    public class Cell : MonoBehaviour {
        [ReadOnly] public int x,y;
        [ReadOnly] public Vector3 originalPos;
        [ReadOnly] public Vector3 originalEulers;

        [Header("Events")]
        public UnityEvent OnMovementShow;
        public UnityEvent OnMovementHide;
        public UnityEvent OnAttackShow;
        public UnityEvent OnAttackHide;
        public UnityEvent OnAttackHighlight;
        public UnityEvent OnMovementHighlight;

        private bool attack, movement;

        public void ShowMovement ( bool value ) {
            movement = value;
            if ( value )
                OnMovementShow.Invoke();
            else
                OnMovementHide.Invoke();
        }

        public void ShowAttack ( bool value ) {
            attack = value;
            if ( value )
                OnAttackShow.Invoke();
            else
                OnAttackHide.Invoke();
        }

        public void Highlight () {
            if ( attack )
                OnAttackHighlight.Invoke();
            if ( movement )
                OnMovementHighlight.Invoke();
        }
    }
}