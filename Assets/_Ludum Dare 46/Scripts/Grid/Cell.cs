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

        public void ShowMovement ( bool value ) {
            if ( value )
                OnMovementShow.Invoke();
            else
                OnMovementHide.Invoke();
        }

        public void ShowAttack ( bool value ) {
            if ( value )
                OnAttackShow.Invoke();
            else
                OnAttackHide.Invoke();
        }
    }
}