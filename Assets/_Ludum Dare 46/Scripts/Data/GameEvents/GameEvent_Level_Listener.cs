namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEvent_Level_Listener : MonoBehaviour {
        public GameEvent_Level gameEvent;
        public UnityEvent_Level response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( Level level ) {
            response.Invoke( level );
        }
    }

    [System.Serializable]
    public class UnityEvent_Level : UnityEvent<Level> { }
}