using UnityEditor;
using UnityEngine;
using Free4GameDevsX2;

namespace Free4GameDevsX2 {

    [CustomEditor(typeof(Free4GameDevsX2))]
    public class Free4GameDevsX2_Editor : Editor
    {

        Free4GameDevsX2 F4GDX2;
        int On = 0;

        void OnEnable()
        {
            F4GDX2 = (Free4GameDevsX2)target;
        }

        private void OnDisable()
        {
            On = 0;
            F4GDX2.RemoveFromEditorCallback();
        }
        private void OnDestroy()
        {
            On = 0;
            F4GDX2.RemoveFromEditorCallback();
        }

        public override void OnInspectorGUI()
        {
            if (On == 1)
            {
                F4GDX2.AddToEditorCallback();
            }
            On++;


                EditorGUILayout.LabelField(":: F4GDX2 SCALING ::");
                EditorGUILayout.LabelField(" Free for all indie developers to use in their games.");
                EditorGUILayout.LabelField(" Open Source / CC0 (See License.txt)");

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Output Filename Addon: ");
                F4GDX2.outputNameAddon = EditorGUILayout.TextField(F4GDX2.outputNameAddon);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Approach: ");
                F4GDX2.approach = (Free4GameDevsX2.Approach)EditorGUILayout.EnumPopup(F4GDX2.approach);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Soften: ");
                F4GDX2.SoftenIt = EditorGUILayout.Toggle(F4GDX2.SoftenIt);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Thicker Lines: ");
                F4GDX2.ThickerLines = EditorGUILayout.Toggle(F4GDX2.ThickerLines);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Chroma Key: ");
                F4GDX2.ProcessChromaColor = EditorGUILayout.Toggle(F4GDX2.ProcessChromaColor);
                EditorGUILayout.EndHorizontal();

                if (F4GDX2.ProcessChromaColor)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField("Remove Chroma Key Color: ");
                    F4GDX2.ChromaKeyColor = EditorGUILayout.ColorField(F4GDX2.ChromaKeyColor);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.LabelField("Scale Amount: ");
                F4GDX2.scaleAmount = (Free4GameDevsX2.ScaleAmount)EditorGUILayout.EnumPopup(F4GDX2.scaleAmount);

                if (F4GDX2.state != Free4GameDevsX2.State.Idle)
                {
                    EditorGUILayout.LabelField("Processing Textures...");
                    if (GUILayout.Button("!! Cancel !!"))
                        F4GDX2.state = Free4GameDevsX2.State.Finish;
                }

                if (GUILayout.Button("Scale"))
                {
                    F4GDX2.state = Free4GameDevsX2.State.Init;
                }


        }
    }

}