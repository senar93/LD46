namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;

    public class Egg : MonoBehaviour {
        [Header("Refs")]
        public Cell cell;

        [Header("Events")]
        public UnityEvent OnSpawn;
        public UnityEvent OnDeath;

        public void Spawn () {
            transform.position = cell.transform.position - Vector3.up * 1;
            OnSpawn.Invoke();
        }

        public void Die () {
            OnDeath.Invoke();
        }
    } 
}