namespace Deirin.Tweeners {
    using UnityEngine;
    using DG.Tweening;

    public class RotationTweener : BaseTweener {
        [Header("Specific Params")]
        public Transform target;
        public Vector3 targetEuler;
        public bool addToOriginal;
        [Tooltip("Always starts from original scale.")]
        public bool resetOnPlay = true;

        private Vector3 startEuler;
        private Vector3 endEuler;

        private void Awake () {
            startEuler = target.localEulerAngles;
        }

        public override void Rewind () {
            target.DOLocalRotate( startEuler, duration ).SetEase( ease ).SetLoops( loops, loopType ).onComplete += () => OnPlayEnd.Invoke();
        }

        public override void Play () {
            OnPlay.Invoke();
            if ( resetOnPlay == false )
                startEuler = target.localScale;
            endEuler = targetEuler;
            target.DOLocalRotate( addToOriginal ? startEuler + endEuler : endEuler, duration ).SetEase( ease ).SetLoops( loops, loopType ).onComplete += () => OnPlayEnd.Invoke();
        }

        public override void Stop () {
            target.DOKill();
            OnPlayEnd.Invoke();
        }
    }
}