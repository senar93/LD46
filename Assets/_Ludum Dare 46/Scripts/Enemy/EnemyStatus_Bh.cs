namespace LD46
{
	using Deirin.EB;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EnemyStatus_Bh : BaseBehaviour
	{
		//lista di tutto quello che può servire per determinare le condizioni del nemico
		[Range(0, 255)]
		public int attackActionIndex = 0;
		[Range(0, 255)]
		public int movementActionIndex = 0;
	}
}