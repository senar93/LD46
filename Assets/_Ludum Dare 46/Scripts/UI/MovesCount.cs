namespace LD46 {
    using UnityEngine;

    public class MovesCount : MonoBehaviour {
        [Header("Prefab")]
        public Transform moveUIPrefab;
        [Header("References")]
        public Transform container;

        public void MovesChangeHandler ( int moves ) {
            for ( int i = 0; i < container.childCount; i++ ) {
                Destroy( container.GetChild( i ).gameObject );
            }

            Instantiate( moveUIPrefab, container );
        }
    }
}