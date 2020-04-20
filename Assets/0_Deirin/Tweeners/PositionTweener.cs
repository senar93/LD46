namespace Deirin.Tweeners {
    using UnityEngine;
    using DG.Tweening;

    public class PositionTweener : BaseTweener {
        [Header("Specific Params")]
        public Transform target;
        public Transform targetTransformPosition;
        public Transform startingTransfrormPosition;
        public Vector3 targetPosition;
        public bool addToOriginal;
        public bool resetOnPlay = true;

        private Vector3 startPos;
        private Vector3 endPos;

        private void Awake () {
            startPos = target.position;
        }

        public override void Rewind () {
            target.DOKill();

            if ( startingTransfrormPosition != null )
                startPos = startingTransfrormPosition.position;

            target.DOMove( startPos, duration ).SetEase( ease ). SetLoops( loops, loopType ).onComplete += () => OnRewindEnd.Invoke();
        }

        public override void Play () {
            target.DOKill();
            OnPlay.Invoke();
            if ( resetOnPlay == false )
                startPos = target.position;

            if ( targetTransformPosition != null ) {
                endPos = targetTransformPosition.position;
            }
            else {
                endPos = addToOriginal ? startPos + targetPosition : targetPosition;
            }

            target.DOMove( endPos, duration ).SetEase( ease ).SetLoops( loops, loopType ).onComplete += () => OnPlayEnd.Invoke();
        }

        public override void Stop () {
            target.DOKill();
            OnStop.Invoke();
        }

        public void GoToEnd () {
            target.DOGoto( duration );
        }
    }
}