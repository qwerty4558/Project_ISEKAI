#if UNITY_EDITOR

using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

namespace Ifooboo
{
    [CustomEditor(typeof(ObjectFade))]
    class ObjectFadeEditor : Editor
    {
        private ObjectFade component;

        private void OnEnable()
        {
            component = (ObjectFade) target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(GUILayout.Button("Preserve Shadows"))
            {
                component.PreserveShadows();

                // ============================================

                var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }
            }
        }
    }
}

#endif