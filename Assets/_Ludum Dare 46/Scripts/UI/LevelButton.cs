namespace LD46 {
    using UnityEngine;
    using TMPro;

    public class LevelButton : MonoBehaviour {
        [Header("Data")]
        public Level level;

        [Header("References")]
        public TextMeshProUGUI text;

        [Header("Events")]
        public UnityEvent_Level OnClick;

        public void Setup ( Level level ) {
            this.level = level;
            text.text = level.name;
        }

        public void Click () {
            OnClick.Invoke( level );
        }
    }
}