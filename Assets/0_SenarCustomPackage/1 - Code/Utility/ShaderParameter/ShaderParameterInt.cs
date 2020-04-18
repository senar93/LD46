namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using UnityEngine.Events;


	public class ShaderParameterInt : AbsShaderParameter<int>
	{

		[ShowInInspector, ShowIf("CanSetMaterialParameter")]
		public override int Value {
			get {
				return CanSetMaterialParameter() ? targetMaterial.GetInt(shaderParameterName) : -9999;
			}
			set {
				if (targetMaterial != null && targetMaterial.HasProperty(shaderParameterName))
				{
					onValueChange?.Invoke();
					targetMaterial?.SetInt(shaderParameterName, value);
				}
			}
		}

		public ShaderParameterInt(Material material, string shaderParameterName, int startingValue)
		{
			targetMaterial = material;
			this.shaderParameterName = shaderParameterName;
			Value = startingValue;
		}

	}
}