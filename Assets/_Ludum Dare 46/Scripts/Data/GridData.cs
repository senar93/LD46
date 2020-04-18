namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;

    [CreateAssetMenu( menuName = "LD46/Grid" )]
    public class GridData : SerializedScriptableObject {
        [ReadOnly] public Cell[,] cells;
    }
}