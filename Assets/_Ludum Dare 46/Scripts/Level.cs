namespace LD46 {
    using UnityEngine;
    using Sirenix.OdinInspector;
    using DG.Tweening;

    public class Level : MonoBehaviour {
        [Title("Data")]
        public GridData gridData;
        [Title("References")]
        public EnemyEntity[] enemies;
        public Egg egg;
        public Transform hiddenPos;

        public System.Action OnActivationSequenceEnd;

        public void Setup () {
            foreach ( var cell in gridData.cells ) {
                cell.originalPos = cell.transform.position;
                cell.transform.position = hiddenPos.position;
                cell.transform.localRotation = Quaternion.Euler( Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ) );
            }
        }

        public float Activate () {
            Sequence s = DOTween.Sequence();

            Cell c = gridData.cells[0,0];
            s.Append( c.transform.DOMove( c.originalPos, 1f ).SetEase( Ease.OutQuint ) );
            s.Join( c.transform.DOLocalRotate( Vector3.zero, 1f ).SetEase( Ease.OutQuint ) );

            for ( int x = 0; x < gridData.cells.GetLength( 0 ); x++ ) {
                for ( int y = 0; y < gridData.cells.GetLength( 1 ); y++ ) {
                    if ( x == 0 && y == 0 )
                        continue;
                    c = gridData.cells[x, y];
                    s.Insert( (x+y) * .2f, c.transform.DOMove( c.originalPos, 1.4f ).SetEase( Ease.OutQuint ) );
                    s.Join( c.transform.DOLocalRotate( Vector3.zero, 1.4f ).SetEase( Ease.OutQuint ) );
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