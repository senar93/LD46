namespace LD46 {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "LD46/Global Game Events/Level" )]
    public class GameEvent_Level : ScriptableObject {
        private List<GameEvent_Level_Listener> listeners = new List<GameEvent_Level_Listener>();
        public System.Action<Level> InvokeAction;

        public void Subscribe ( GameEvent_Level_Listener listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEvent_Level_Listener listener ) {
            listeners.Remove( listener );
        }

        public void Invoke ( Level level ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( level );
            }
            InvokeAction?.Invoke( level );
        }
    }
}