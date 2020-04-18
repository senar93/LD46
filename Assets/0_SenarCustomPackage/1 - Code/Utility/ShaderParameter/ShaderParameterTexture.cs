namespace SenarCustomSystem.Utility
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Sirenix.OdinInspector;
	using UnityEngine.Events;


	public class ShaderParameterTexture : AbsShaderParameter<Texture>
	{
		[ShowInInspector, ShowIf("CanSetMaterialParameter")]
		public override Texture Value {
			get {
				return CanSetMaterialParameter() ? targetMaterial.GetTexture(shaderParameterName) : null;
			}
			set {
				if (CanSetMaterialParameter())
				{
					onValueChange?.Invoke();
					targetMaterial?.SetTexture(shaderParameterName, value);
				}
			}
		}

		public ShaderParameterTexture(Material material, string shaderParameterName, Texture startingValue)
		{
			targetMaterial = material;
			this.shaderParameterName = shaderParameterName;
			Value = startingValue;
		}

	}
}