using UnityEditor;
using UnityEngine;

namespace GameName.SOInjection.Editor
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : UnityEditor.Editor
    {
        private GameEvent _instance;

        private void OnEnable()
        {
            _instance = (GameEvent)target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Raise"))
            {
                _instance.Raise();
            }
        }
    }
}