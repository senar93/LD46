namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEvent_Enemy_Listener : MonoBehaviour {
        public GameEvent_Enemy gameEvent;
        public UnityEvent_Enemy response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( EnemyEntity enemy ) {
            response.Invoke( enemy );
        }
    }

    [System.Serializable]
    public class UnityEvent_Enemy : UnityEvent<EnemyEntity> { }
}