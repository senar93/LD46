namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;

    public class EnemyComparer : MonoBehaviour {
        public EnemyEntity thisEnemy;
        public UnityEvent OnTrue;
        public UnityEvent OnFalse;

        public void Compare ( EnemyEntity enemyToCompare ) {
            if ( thisEnemy == enemyToCompare )
                OnTrue.Invoke();
            else
                OnFalse.Invoke();
        }
    }
}