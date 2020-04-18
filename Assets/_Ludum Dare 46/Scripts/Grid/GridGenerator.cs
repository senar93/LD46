namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public class GridGenerator : MonoBehaviour {
        [Title("Data")]
        public GridData gridData;

        [Title("Prefabs")]
        public Cell cellPrefab;

        [Title("Parameters")]
        [MinValue(0)] public Vector2Int size;
        [MinValue(0)] public float cellDistance;

        [Title("References")]
        public Transform container;

        [Button( "Generate" )]
        public void Generate () {
            int count = container.childCount;
            for ( int i = 0; i < count; i++ ) {
                if ( Application.isPlaying ) {
                    Destroy( container.GetChild( i ).gameObject );
                }
                else {
                    DestroyImmediate( container.GetChild( 0 ).gameObject );
                }
            }

            gridData.cells = new Cell[size.x, size.y];
            Vector3 startingOffset = new Vector3( -size.x * .5f * cellDistance, 0, -size.y * .5f * cellDistance );
            print( startingOffset );
            for ( int x = 0; x < size.x; x++ ) {
                for ( int y = 0; y < size.y; y++ ) {
                    Vector3 specificOffset = new Vector3( x * cellDistance + cellDistance * .5f, 0, y * cellDistance + cellDistance * .5f );
                    Cell c = Instantiate( cellPrefab,
                        container.transform.position + startingOffset + specificOffset,
                        Quaternion.identity, container );
                    c.x = x;
                    c.y = y;
                    gridData.cells[x, y] = c;
#if UNITY_EDITOR
                    EditorUtility.SetDirty( gridData );
                    AssetDatabase.SaveAssets();
#endif
                }
            }
        }
    }
}