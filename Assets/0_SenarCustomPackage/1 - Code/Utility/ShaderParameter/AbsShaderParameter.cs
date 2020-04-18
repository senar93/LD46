namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using UnityEngine.Events;


	public abstract class AbsShaderParameter<T>
	{
		public Material targetMaterial;
		public string shaderParameterName;

		public abstract T Value { get; set; }

		public UnityEvent onValueChange;

		public bool CanSetMaterialParameter()
		{
			return targetMaterial != null && targetMaterial.HasProperty(shaderParameterName);
		}
	}
}