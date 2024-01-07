using UnityEditor;

namespace GameName.MazeSystem
{
    [CustomEditor(typeof(LevelGenerator))]
    public sealed class LevelGeneratorEditor : Editor
    {
        // i like to keep these ones same name in their original script, that's why no underscore
        private SerializedProperty cellPrefab; 
        private SerializedProperty mazeGridSize;
        private SerializedProperty cellPrefabScale;
        private SerializedProperty debugMode;
        private SerializedProperty useRandomSeed;
        private SerializedProperty seed;
        private SerializedProperty minDistanceThreshold;
        private SerializedProperty endPointPrefab;
        private SerializedProperty playerPrefab;
        private SerializedProperty enemyPrefab;
        private SerializedProperty mazeScale;
        
        private void OnEnable()
        {
            cellPrefab = serializedObject.FindProperty(nameof(cellPrefab));
            mazeGridSize = serializedObject.FindProperty(nameof(mazeGridSize));
            cellPrefabScale = serializedObject.FindProperty(nameof(cellPrefabScale));
            seed = serializedObject.FindProperty(nameof(seed));
            useRandomSeed = serializedObject.FindProperty(nameof(useRandomSeed));
            debugMode = serializedObject.FindProperty(nameof(debugMode));
            minDistanceThreshold = serializedObject.FindProperty(nameof(minDistanceThreshold));
            endPointPrefab = serializedObject.FindProperty(nameof(endPointPrefab));
            playerPrefab = serializedObject.FindProperty(nameof(playerPrefab));
            enemyPrefab = serializedObject.FindProperty(nameof(enemyPrefab));
            mazeScale = serializedObject.FindProperty(nameof(mazeScale));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(mazeScale);
            EditorGUILayout.PropertyField(cellPrefab);
            EditorGUILayout.PropertyField(mazeGridSize);
            EditorGUILayout.PropertyField(cellPrefabScale);
            EditorGUILayout.PropertyField(debugMode);
            EditorGUILayout.PropertyField(minDistanceThreshold);
            EditorGUILayout.PropertyField(endPointPrefab);
            EditorGUILayout.PropertyField(playerPrefab);
            EditorGUILayout.PropertyField(enemyPrefab);

            if (useRandomSeed.boolValue)
                EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(useRandomSeed);
            if (useRandomSeed.boolValue)
            {
                EditorGUILayout.LabelField("Using Randomized Seed");
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.PropertyField(seed);
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}