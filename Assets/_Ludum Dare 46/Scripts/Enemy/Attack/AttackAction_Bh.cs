namespace LD46 {
    using Deirin.EB;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class AttackAction_Bh : AbsAction_Bh {
        //pattern attacco da effettuare
        [Title("Parameters"), Range(1, 10), SerializeField, HideIf("attackPattern")]
        private int radius;

        [Title("Parameters"), SerializeField, ShowIf("attackPattern")]
        private bool[,] attackPattern;

        [Title("Next Attack Index"), Range(0, 255), SerializeField, Space]
        private int nextAttackIndexValue = 0;

        public Cell[] SelectedCells {
            get {
                EnemyEntity enemyEntity = (Entity as EnemyEntity);
                //fetch grid size
                Vector2Int gridSize = new Vector2Int();
                gridSize.x = enemyEntity.currentGrid.cells.GetLength( 0 );
                gridSize.y = enemyEntity.currentGrid.cells.GetLength( 1 );
                //fetch current position
                Vector2Int cp = new Vector2Int();
                cp.Set( enemyEntity.cell.x, enemyEntity.cell.y );
                //Debug.LogError(cp);

                //Get and rotate real attack pattern
                bool[,] realAttackPattern = GetAttackPatternCopy();
                RotateRealAttackPattern( realAttackPattern );

                return GetTargettedCellsCoords( realAttackPattern, cp, gridSize ).ToArray();
            }
        }


        public override void SetNextIndex () {
            ( ( EnemyEntity ) Entity ).attackActionIndex = nextAttackIndexValue;
        }


        #region EDITOR
        [Button( "Set Attack Pattern", ButtonSizes.Large ), HideIf( "attackPattern" ), GUIColor( 0.5f, 1f, 0.5f, 1f )]
        private void SetAttackPattern () {
            attackPattern = new bool[radius * 2 + 1, radius * 2 + 1];
        }

        [Button( "Destory Attack Pattern", ButtonSizes.Medium ), ShowIf( "attackPattern" ), GUIColor( 1f, 0.5f, 0.5f, 1f )]
        private void DestroyAttackPattern () {
            attackPattern = null;
        }

        [Button( "Rotate Clockwise!", ButtonSizes.Small ), ShowIf( "attackPattern" ), GUIColor( 1f, 1f, 0.9f, 1f )]
        private void RotateClockwise () {
            attackPattern.RotateMatrix( radius * 2 + 1 );
        }

        [Button( "Rotate Unclockwise!", ButtonSizes.Small ), ShowIf( "attackPattern" ), GUIColor( 1f, 1f, 0.9f, 1f )]
        private void RotateUnclockwise () {
            attackPattern.RotateMatrix( radius * 2 + 1 );
            attackPattern.RotateMatrix( radius * 2 + 1 );
            attackPattern.RotateMatrix( radius * 2 + 1 );
        }

        #endregion


        #region INTERNAL FUNCTION
        private bool[,] GetAttackPatternCopy () {
            bool[,] realAttackPattern = new bool[radius * 2 + 1, radius * 2 + 1];
            for ( int i = 0; i < realAttackPattern.GetLength( 0 ); i++ ) {
                for ( int j = 0; j < realAttackPattern.GetLength( 1 ); j++ ) {
                    realAttackPattern[i, j] = attackPattern[i, j];
                }
            }
            return realAttackPattern;
        }

        private void RotateRealAttackPattern ( bool[,] realAttackPattern ) {
            switch ( ( Entity as EnemyEntity ).enemyDirection ) {
                case DirectionEnum.Down:
                break;
                case DirectionEnum.Right:
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                break;
                case DirectionEnum.Up:
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                break;
                case DirectionEnum.Left:
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                realAttackPattern.RotateMatrix( radius * 2 + 1 );
                break;
            }
        }

        private bool ValidateGridCoords ( int x, int y, Vector2Int gridSize ) {
            return x >= 0 &&
                   y >= 0 &&
                   x < gridSize.x &&
                   y < gridSize.y;
        }

        private List<Cell> GetTargettedCellsCoords ( bool[,] realAttackPattern, Vector2Int cp, Vector2Int gridSize ) {
            List<Cell> tmpList = new List<Cell>();
            EnemyEntity enemyEntity = (Entity as EnemyEntity);
            Vector2Int realOffset = new Vector2Int(cp.x - (realAttackPattern.GetLength(0) / 2),
                                                   cp.y - (realAttackPattern.GetLength(1) / 2));


            for ( int xLoc = 0; xLoc < realAttackPattern.GetLength( 0 ); xLoc++ ) {
                for ( int yLoc = 0; yLoc < realAttackPattern.GetLength( 1 ); yLoc++ ) {
                    //la cella presa in considerazione nel pattern è vera e appartiene alla griglia
                    if ( realAttackPattern[xLoc, yLoc] == true &&
                        ValidateGridCoords( xLoc + realOffset.x,
                                           yLoc + realOffset.y,
                                           gridSize ) ) {
                        tmpList.Add( enemyEntity.currentGrid.cells[xLoc + realOffset.x,
                                                                  yLoc + realOffset.y] );
                    }
                }
            }

            return tmpList;
        }
        #endregion

    }


    public static class MatrixExtention {
        // An Inplace function to 
        // rotate a N x N matrix 
        // by 90 degrees in anti- 
        // clockwise direction 
        public static void RotateMatrix<T> ( this T[,] mat, int N ) {
            // Consider all 
            // squares one by one 
            for ( int x = 0; x < N / 2; x++ ) {
                // Consider elements 
                // in group of 4 in 
                // current square 
                for ( int y = x; y < N - x - 1; y++ ) {
                    // store current cell 
                    // in temp variable 
                    T temp = mat[x, y];

                    // move values from 
                    // right to top 
                    mat[x, y] = mat[y, N - 1 - x];

                    // move values from 
                    // bottom to right 
                    mat[y, N - 1 - x] = mat[N - 1 - x,
                                            N - 1 - y];

                    // move values from 
                    // left to bottom 
                    mat[N - 1 - x,
                        N - 1 - y]
                        = mat[N - 1 - y, x];

                    // assign temp to left 
                    mat[N - 1 - y, x] = temp;
                }
            }
        }
    }


}