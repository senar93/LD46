namespace LD46 {
    using UnityEngine;
    using UnityEngine.Events;
    using Sirenix.OdinInspector;
    using DG.Tweening;
    using System.Collections.Generic;
	using System.Linq;

    public class Level : SerializedMonoBehaviour {
        [Title("Data")]
        [ReadOnly] public GridData gridData;
        [Min(1)] public int turns;

        [Title("References")]
        public List<EnemyEntity> enemies;
        public Egg egg;
        public Transform hiddenPos;

        [Header("Events")]
        public UnityEvent OnSetupStart;
        public UnityEvent OnSetupEnd;

		[HideInInspector]
        public System.Action OnActivationSequenceEnd;
        public bool EnemiesLeft => enemies.Count > 0;

        public void Setup () {
            OnSetupStart.Invoke();
            foreach ( var cell in gridData.cells ) {
                cell.originalPos = cell.transform.position;
                cell.originalEulers = cell.transform.localEulerAngles;
                cell.transform.position = hiddenPos.position;
                cell.transform.localRotation = Quaternion.Euler( Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ) );
            }
            foreach ( var enemy in enemies ) {
                enemy.transform.position = hiddenPos.position;
                enemy.egg = egg;
                enemy.currentGrid = gridData;
            }
            egg.transform.position = hiddenPos.position;
            OnSetupEnd.Invoke();
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

        public void EnemyDeathHandler ( EnemyEntity enemy ) {
            enemies.Remove( enemy );
        }

        private void ActivationSequenceEndHandler () {
            OnActivationSequenceEnd?.Invoke();
        }

		#region EDITOR
		//[Button("Fill Enemies & Egg", ButtonSizes.Gigantic), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void FillEnemiesAndEgg()
		{
			enemies.Clear();
			enemies = GetComponentsInChildren<EnemyEntity>().ToList();
			egg = GetComponentInChildren<Egg>();
		}

		//[Button("Add to Game State Machine", ButtonSizes.Gigantic), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void AddToGameStateMachine()
		{
			Game_SM tmp = FindObjectOfType<Game_SM>();
			if(!tmp.context.levels.Contains(this))
			{
				tmp.context.levels.Add(this);
			}
		}

		[Button("SETUP LEVEL", ButtonSizes.Gigantic), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void SetupLevelRef_EditorOnly()
		{
			FillEnemiesAndEgg();
			AddToGameStateMachine();
		}
		#endregion

	}
}