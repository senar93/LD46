namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;

    [CreateAssetMenu( menuName = "LD46/Grid" )]
    [System.Serializable]
    public class GridData : SerializedScriptableObject {
        [ReadOnly] public Cell[,] cells;
    }
}