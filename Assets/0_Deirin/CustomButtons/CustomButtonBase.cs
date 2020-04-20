namespace Deirin.CustomButton {
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class CustomButtonBase : MonoBehaviour {
        public UnityEvent OnActiveTrue;
        public UnityEvent OnActiveFalse;

        public System.Action OnSelection, OnDeselection, OnClick;

        protected bool selected;
        public bool active;

        public void Active ( bool value ) {
            if ( value == active )
                return;
            active = value;
            if ( active )
                OnActiveTrue.Invoke();
            else
                OnActiveFalse.Invoke();
        }

        public void Select () {
            if ( !active )
                return;

            if ( selected )
                return;

            selected = true;
            OnSelection?.Invoke();
        }

        public void Deselect () {
            if ( !active )
                return;

            if ( !selected )
                return;

            selected = false;
            OnDeselection?.Invoke();
        }

        public void Click () {
            if ( !active )
                return;

            if ( !selected )
                return;

            OnClick?.Invoke();
        }
    }
}