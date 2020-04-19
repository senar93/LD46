namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class Cell : MonoBehaviour {
        [ReadOnly] public int x,y;
        [ReadOnly] public Vector3 originalPos;
        [ReadOnly] public Vector3 originalEulers;
    }
}