namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using UnityEngine.Events;


	public class ShaderParameterColor : AbsShaderParameter<Color>
	{
		[ShowInInspector, ShowIf("CanSetMaterialParameter")]
		public override Color Value {
			get {
				return CanSetMaterialParameter() ? targetMaterial.GetColor(shaderParameterName) : Color.clear;
			}
			set {
				if (CanSetMaterialParameter())
				{
					onValueChange?.Invoke();
					targetMaterial?.SetColor(shaderParameterName, value);
				}
			}
		}


		public ShaderParameterColor(Material material, string shaderParameterName, Color startingValue)
		{
			targetMaterial = material;
			this.shaderParameterName = shaderParameterName;
			Value = startingValue;
		}

	}
}