using UnityEditor;
using UnityEngine;
using Free4GameDevsX2;

namespace Free4GameDevsX2 {

    [CustomEditor(typeof(Free4GameDevsX2))]
    public class Free4GameDevsX2_Editor : Editor
    {

        Free4GameDevsX2 F4GDX2;

        void OnEnable()
        {
            F4GDX2 = (Free4GameDevsX2)target;
        }

        public override void OnInspectorGUI()
        {

                EditorGUILayout.LabelField(":: F4GDX2 SCALING ::");
                EditorGUILayout.LabelField(" Free for all indie developers to use in their games.");
                EditorGUILayout.LabelField(" Open Source / CC0 (See License.txt)");

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Output Filename Addon: ");
                F4GDX2.outputNameAddon = EditorGUILayout.TextField(F4GDX2.outputNameAddon);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Soften: ");
                F4GDX2.SoftenIt = EditorGUILayout.Toggle(F4GDX2.SoftenIt);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Thicker Lines: ");
                F4GDX2.ThickerLines = EditorGUILayout.Toggle(F4GDX2.ThickerLines);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("Scale Amount: ");
                F4GDX2.scaleAmount = (Free4GameDevsX2.ScaleAmount)EditorGUILayout.EnumPopup(F4GDX2.scaleAmount);
  
              
                if (GUILayout.Button("Scale"))
                {
                    F4GDX2.StartUp();
                }

        }
    }

}