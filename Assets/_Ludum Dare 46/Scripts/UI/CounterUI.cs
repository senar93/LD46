namespace LD46 {
    using UnityEngine;

    public class CounterUI : MonoBehaviour {
        [Header("Prefab")]
        public Transform prefab;
        [Header("References")]
        public Transform container;

        public void UpdateUI ( int amount ) {
            for ( int i = 0; i < container.childCount; i++ ) {
                Destroy( container.GetChild( i ).gameObject );
            }

            for ( int i = 0; i < amount; i++ ) {
                Instantiate( prefab, container );
            }
        }
    }
}