namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class Level : MonoBehaviour {
        [Title("Data")]
        public GridData gridData;
        [Title("References")]
        public EnemyEntity[] enemies;

        public void Activate () {

        }
    }
}