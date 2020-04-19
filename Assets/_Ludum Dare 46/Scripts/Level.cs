namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;
    using DG.Tweening;
    using System.Collections.Generic;

    public class Level : MonoBehaviour {
        [Title("Data")]
        public GridData gridData;
        [Min(0)] public int turns;

        [Title("References")]
        public List<EnemyEntity> enemies;
        public Egg egg;
        public Transform hiddenPos;

        public System.Action OnActivationSequenceEnd;
        public bool EnemiesLeft => enemies.Count > 0;

        public void Setup () {
            foreach ( var cell in gridData.cells ) {
                cell.originalPos = cell.transform.position;
                cell.originalEulers = cell.transform.localEulerAngles;
                cell.transform.position = hiddenPos.position;
                cell.transform.localRotation = Quaternion.Euler( Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ) );
            }
            foreach ( var enemy in enemies ) {
                enemy.transform.position = hiddenPos.position;
            }
            egg.transform.position = hiddenPos.position;
        }

        public float Activate () {
            Sequence s = DOTween.Sequence();

            Cell c = gridData.cells[0,0];
            s.Append( c.transform.DOMove( c.originalPos, 1f ).SetEase( Ease.OutQuint ) );
            s.Join( c.transform.DOLocalRotate( c.originalEulers, 1f ).SetEase( Ease.OutQuint ) );

            for ( int x = 0; x < gridData.cells.GetLength( 0 ); x++ ) {
                for ( int y = 0; y < gridData.cells.GetLength( 1 ); y++ ) {
                    if ( x == 0 && y == 0 )
                        continue;
                    c = gridData.cells[x, y];
                    s.Insert( ( x + y ) * .2f, c.transform.DOMove( c.originalPos, 1.4f ).SetEase( Ease.OutQuint ) );
                    s.Join( c.transform.DOLocalRotate( c.originalEulers, 1.4f ).SetEase( Ease.OutQuint ) );
                }
            }
            s.onComplete += ActivationSequenceEndHandler;

            return s.Duration();
        }

        private void ActivationSequenceEndHandler () {
            OnActivationSequenceEnd?.Invoke();
        }
    }
}