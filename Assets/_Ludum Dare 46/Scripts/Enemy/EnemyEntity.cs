namespace LD46 {
    using Deirin.EB;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Events;
    using DG.Tweening;

    public class EnemyEntity : BaseEntity {
        [Header("References")]
        public Transform fireBallSpawnPoint;
        public Interpolator fireBallPrefab;

        public UnityEvent_Enemy OnDeath;
        [Header("Movement")]
        public UnityEvent OnMoveStart;
        public UnityEvent_Enemy OnMoveEnd;
        [Header("Rotation")]
        public UnityEvent OnRotateRight;
        public UnityEvent OnRotateLeft;
        public UnityEvent_Enemy OnRotateEnd;
        public UnityEvent OnPlayerRotationEnd;
        [Header("Attack")]
        public UnityEvent OnAttackStart;
        public UnityEvent_Enemy OnAttackEnd;

        [Range(0, 255)]
        public int attackActionIndex = 0;
        [Range(0, 255)]
        public int movementActionIndex = 0;
        [Range(0, 255)]
        public int rotationActionIndex = 0;

        [HideInInspector]
        public Cell cell;
        [HideInInspector]
        public DirectionEnum enemyDirection = DirectionEnum.Down;
        [HideInInspector] public GridData currentGrid;
        [HideInInspector] public Level level;

        [ShowInInspector]
        private Cell InspectorCell {
            get => cell;
            set {
                cell = value;
                this.transform.position = cell.transform.position + Vector3.up * 0.5f;
            }
        }
        [ShowInInspector]
        private DirectionEnum InspectorEnemyDirection {
            get {
                return enemyDirection;
            }
            set {
                enemyDirection = value;
                switch ( enemyDirection ) {
                    case DirectionEnum.Down:
                    transform.rotation = Quaternion.Euler( 0, 225, 0 );
                    break;
                    case DirectionEnum.Right:
                    transform.rotation = Quaternion.Euler( 0, 135, 0 );
                    break;
                    case DirectionEnum.Up:
                    transform.rotation = Quaternion.Euler( 0, 405, 0 );
                    break;
                    case DirectionEnum.Left:
                    transform.rotation = Quaternion.Euler( 0, 315, 0 );
                    break;
                }
            }
        }
        private int attackCount;
        private Interpolator tempInterpolator;
        private Cell[] tempCells;

        protected override void CustomSetup () {
            transform.position = cell.originalPos + Vector3.up * .5f;
        }

        #region API Gameplay
        public void Die () {
            OnDeath.Invoke( this );
            Destroy( gameObject );
        }

        public void Attack () {
            OnAttackStart.Invoke();

            tempCells = GetAttackTargets();
            attackCount = tempCells.Length;

            foreach ( var cell in tempCells ) {
                tempInterpolator = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, Quaternion.identity);
                tempInterpolator.OnTargetReached.AddListener( () => FireBallReachedPositionHandler( cell ) );
                tempInterpolator.SetTarget( cell.transform );
                tempInterpolator.StartInterpolation();
            }

            GoToNextAttackAction();
        }

        public void Move () {
            //move and check egg
            Sequence s = DOTween.Sequence();
            foreach ( var cell in GetMovementCells() ) {
                s.Append( transform.DOMove( cell.transform.position + Vector3.up * .5f, 1.5f ).SetEase( Ease.Linear ) );
                s.AppendCallback( () => CheckCell( cell ) );
            }
            s.onComplete += () => OnMoveEnd.Invoke( this );
            GoToNextMovementAction();

            void CheckCell ( Cell c ) {
                cell = c;
                if ( c == level.egg.cell )
                    level.egg.Die();
            }

            OnMoveStart.Invoke();
        }

        public void Rotate () {
            switch ( GetRotationAction() ) {
                case RotateActionEnum.TurnLeft:
                OnRotateLeft.Invoke();
                transform.DOLocalRotate( transform.localEulerAngles + Vector3.up * -90f, 1.333f ).SetEase( Ease.Linear ).onComplete += () => OnRotateEnd.Invoke( this );
                break;
                case RotateActionEnum.TurnRight:
                OnRotateRight.Invoke();
                transform.DOLocalRotate( transform.localEulerAngles + Vector3.up * 90f, 1.333f ).SetEase( Ease.Linear ).onComplete += () => OnRotateEnd.Invoke( this );
                break;
                case RotateActionEnum.Turn180:
                transform.DOLocalRotate( transform.localEulerAngles + Vector3.up * 180f, 1.333f ).SetEase( Ease.Linear ).onComplete += () => OnRotateEnd.Invoke( this );
                break;
                case RotateActionEnum.none:
                OnRotateEnd.Invoke( this );
                break;
            }
            enemyDirection = GetNewGlobalDirection();
            GoToNextRotationAction();
        }

        public void RotateLeft () {
            OnRotateLeft.Invoke();
            transform.DOLocalRotate( transform.localEulerAngles + Vector3.up * -90f, 1.333f ).SetEase( Ease.Linear ).onComplete += () => OnPlayerRotationEnd.Invoke();
            enemyDirection = NewEntityDirection( RotateActionEnum.TurnLeft );
        }

        public void RotateRight () {
            OnRotateRight.Invoke();
            transform.DOLocalRotate( transform.localEulerAngles + Vector3.up * 90f, 1.333f ).SetEase( Ease.Linear ).onComplete += () => OnPlayerRotationEnd.Invoke();
            enemyDirection = NewEntityDirection( RotateActionEnum.TurnRight );
        }
        #endregion

        #region API GET SOMETHING
        /// <summary>
        /// la lista delle celle in cui si muoverà l'entity, ordinate
        /// </summary>
        /// <returns></returns>
        public Cell[] GetMovementCells () {
            MoveManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                return tmp.CurrentAction.SelectedCells;
            }

            return null;
        }

        /// <summary>
        /// l'ultima cella in cui si muoverà l'entity
        /// </summary>
        /// <returns></returns>
        public Cell GetMovementLastCell () {
            MoveManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                return tmp.CurrentAction.SelectedCells[tmp.CurrentAction.SelectedCells.Length - 1];
            }

            return null;
        }

        /// <summary>
        /// Restituisce la lista delle celle che verranno attaccate da questo nemico
        /// </summary>
        /// <returns></returns>
        public Cell[] GetAttackTargets () {
            AttackManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                return tmp.CurrentAction.SelectedCells;
            }

            return null;
        }

        /// <summary>
        /// da che lato sta ruotando entity
        /// </summary>
        /// <returns></returns>
        public RotateActionEnum GetRotationAction () {
            RotationManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                return tmp.CurrentAction.rotation;
            }

            return RotateActionEnum.none;
        }

        /// <summary>
        /// la vera rotazione di entity
        /// </summary>
        /// <returns></returns>
        public DirectionEnum GetNewGlobalDirection () {
            RotationManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                return tmp.CurrentAction.NewEntityDirection;
            }

            return enemyDirection;
        }

        #endregion

        #region API INDEX UPDATE
        public void GoToNextMovementAction () {
            MoveManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                tmp.GoToNextAction();
            }
        }

        public void GoToNextAttackAction () {
            AttackManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                tmp.GoToNextAction();
            }
        }

        public void GoToNextRotationAction () {
            RotationManager_Bh tmp;
            if ( TryGetBehaviour( out tmp ) ) {
                tmp.GoToNextAction();
            }
        }

        #endregion


        [Button( "TEST!", ButtonSizes.Large ), GUIColor( 0.5f, 1f, 0.5f, 1f )]
        private void SetAttackPattern () {
            Cell[] cells = GetAttackTargets();//GetCurrentMovementCells();
#if UNITY_EDITOR
            Debug.Log( "Player Position: " + cell.x + "," + cell.y );
            Debug.Log( "Lenght: " + cells.Length );
            for ( int i = 0; i < cells.Length; i++ )
                Debug.Log( "Cells[ " + cells[i].x + " , " + cells[i].y + " ]" );
#endif
        }

        private DirectionEnum NewEntityDirection ( RotateActionEnum rotation ) {
            switch ( enemyDirection ) {
                case DirectionEnum.Down:
                switch ( rotation ) {
                    case RotateActionEnum.TurnLeft:
                    return DirectionEnum.Right;
                    case RotateActionEnum.TurnRight:
                    return DirectionEnum.Left;
                    case RotateActionEnum.Turn180:
                    return DirectionEnum.Up;
                }
                break;
                case DirectionEnum.Right:
                switch ( rotation ) {
                    case RotateActionEnum.TurnLeft:
                    return DirectionEnum.Up;
                    case RotateActionEnum.TurnRight:
                    return DirectionEnum.Down;
                    case RotateActionEnum.Turn180:
                    return DirectionEnum.Left;
                }
                break;
                case DirectionEnum.Up:
                switch ( rotation ) {
                    case RotateActionEnum.TurnLeft:
                    return DirectionEnum.Left;
                    case RotateActionEnum.TurnRight:
                    return DirectionEnum.Right;
                    case RotateActionEnum.Turn180:
                    return DirectionEnum.Down;
                }
                break;
                case DirectionEnum.Left:
                switch ( rotation ) {
                    case RotateActionEnum.TurnLeft:
                    return DirectionEnum.Down;
                    case RotateActionEnum.TurnRight:
                    return DirectionEnum.Up;
                    case RotateActionEnum.Turn180:
                    return DirectionEnum.Right;
                }
                break;
            }

            return enemyDirection;
        }

        private void FireBallReachedPositionHandler ( Cell c ) {
            attackCount--;

            foreach ( var enemy in level.enemies ) {
                if ( level.egg.cell == c ) {
                    level.egg.Die();
                }

                if ( enemy.cell == c ) {
                    enemy.Die();
                }
            }

            if ( attackCount == 0 )
                OnAttackEnd.Invoke( this );
        }
    }

    public static class MonoBehaviourExtentions {
        public static Vector3[] ToPositions ( this MonoBehaviour[] monos ) {
            Vector3[] ts = new Vector3[monos.Length];
            for ( int i = 0; i < monos.Length; i++ ) {
                ts[i] = monos[i].transform.position;
            }
            return ts;
        }

        public static Vector3[] AddPosition ( this Vector3[] positions, Vector3 value ) {
            for ( int i = 0; i < positions.Length; i++ ) {
                positions[i] += value;
            }
            return positions;
        }
    }
}