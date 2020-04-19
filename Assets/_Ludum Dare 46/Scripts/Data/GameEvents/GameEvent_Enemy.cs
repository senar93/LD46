namespace LD46 {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "LD46/Global Game Events/Enemy" )]
    public class GameEvent_Enemy : ScriptableObject {
        private List<GameEvent_Enemy_Listener> listeners = new List<GameEvent_Enemy_Listener>();
        public System.Action<EnemyEntity> InvokeAction;

        public void Subscribe ( GameEvent_Enemy_Listener listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEvent_Enemy_Listener listener ) {
            listeners.Remove( listener );
        }

        public void Invoke ( EnemyEntity enemy ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( enemy );
            }
            InvokeAction?.Invoke( enemy );
        }
    }
}