namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;

	public class AttackAction_Bh : AbsAction_Bh
	{
		//pattern attacco da effettuare
		[Title("Parameters"), Range(1, 10), SerializeField, HideIf("attackPattern")]
		private int radius;

		[Title("Parameters"), SerializeField, ShowIf("attackPattern")]
		private bool[,] attackPattern;

		/// <summary>
		/// WIP
		/// </summary>
		public Vector2Int[] selectedCells {
			get {
				List<Vector2Int> tmp = new List<Vector2Int>();


				return null;
			}
		}


		[Title("Next Attack Index"), Range(0, 255), SerializeField, Space]
		private int nextAttackIndexValue = 0;
		public override void SetNextIndex()
		{
			((EnemyEntity)Entity).attackActionIndex = nextAttackIndexValue;
		}


		#region EDITOR
		[Button("Set Attack Pattern", ButtonSizes.Large), HideIf("attackPattern"), GUIColor(0.5f, 1f, 0.5f, 1f)]
		private void SetAttackPattern()
		{
			attackPattern = new bool[radius * 2 + 1, radius * 2 + 1];
		}

		[Button("Destory Attack Pattern", ButtonSizes.Medium), ShowIf("attackPattern"), GUIColor(1f, 0.5f, 0.5f, 1f)]
		private void DestroyAttackPattern()
		{
			attackPattern = null;
		}

		[Button("Rotate Clockwise!", ButtonSizes.Small), ShowIf("attackPattern"), GUIColor(1f, 1f, 0.9f, 1f)]
		private void RotateClockwise()
		{
			attackPattern.RotateMatrix(radius * 2 + 1);
		}

		[Button("Rotate Unclockwise!", ButtonSizes.Small), ShowIf("attackPattern"), GUIColor(1f, 1f, 0.9f, 1f)]
		private void RotateUnclockwise()
		{
			attackPattern.RotateMatrix(radius * 2 + 1);
			attackPattern.RotateMatrix(radius * 2 + 1);
			attackPattern.RotateMatrix(radius * 2 + 1);
		}

		#endregion


	}


	public static class MatrixExtention {
		// An Inplace function to 
		// rotate a N x N matrix 
		// by 90 degrees in anti- 
		// clockwise direction 
		public static void RotateMatrix<T>(this T[,] mat, int N)
		{
			// Consider all 
			// squares one by one 
			for (int x = 0; x < N / 2; x++)
			{
				// Consider elements 
				// in group of 4 in 
				// current square 
				for (int y = x; y < N - x - 1; y++)
				{
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