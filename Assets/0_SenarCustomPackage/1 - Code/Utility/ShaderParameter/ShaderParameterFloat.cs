namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using UnityEngine.Events;


	public class ShaderParameterFloat : AbsShaderParameter<float>
	{

		[ShowInInspector, ShowIf("CanSetMaterialParameter")]
		public override float Value {
			get {
				return CanSetMaterialParameter() ? targetMaterial.GetFloat(shaderParameterName) : -9999;
			}
			set {
				if (CanSetMaterialParameter())
				{
					onValueChange?.Invoke();
					targetMaterial?.SetFloat(shaderParameterName, value);
				}
			}
		}

		public ShaderParameterFloat(Material material, string shaderParameterName, float startingValue)
		{
			targetMaterial = material;
			this.shaderParameterName = shaderParameterName;
			Value = startingValue;
		}

	}
}