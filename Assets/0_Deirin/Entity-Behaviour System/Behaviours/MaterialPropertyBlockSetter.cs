namespace Deirin.EB {
    using UnityEngine;

    public class MaterialPropertyBlockSetter : BaseBehaviour {
        [Header("Refs")]
        public Renderer[] targetRenderers;

        [Header("Params")]
        public bool setupOnAwake = true;
        public MaterialParameter[] parameters;

        private void Awake () {
            if ( setupOnAwake )
                Setup();
        }

        public void Setup () {
            foreach ( var item in parameters ) {
                item.Setup();
            }
        }

        public void SetMPB () {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            for ( int i = 0; i < parameters.Length; i++ ) {
                switch ( parameters[i].type ) {
                    case MaterialParameter.Type.Color:
                    mpb.SetColor( parameters[i].id, parameters[i].colorValue );
                    break;
                    case MaterialParameter.Type.Float:
                    mpb.SetFloat( parameters[i].id, parameters[i].floatValue );
                    break;
                    case MaterialParameter.Type.Int:
                    mpb.SetInt( parameters[i].id, parameters[i].intValue );
                    break;
                    case MaterialParameter.Type.Vector4:
                    mpb.SetVector( parameters[i].id, parameters[i].vector4Value );
                    break;
                }
            }
            for ( int i = 0; i < targetRenderers.Length; i++ ) {
                targetRenderers[i].SetPropertyBlock( mpb );
            }
        }
    }

    [System.Serializable]
    public class MaterialParameter {
        [HideInInspector] public int id;
        public enum Type { Color, Float, Int, Vector4 }
        public Type type;
        public string name;
        public Color colorValue;
        public float floatValue;
        public int intValue;
        public Vector4 vector4Value;

        public void Setup () {
            id = Shader.PropertyToID( name );
        }
    }
}