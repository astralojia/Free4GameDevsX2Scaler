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

                EditorGUILayout.BeginVertical("box");


                EditorGUILayout.LabelField(":: F4GDX2 SCALING ::");
                EditorGUILayout.LabelField(" Free for all indie developers to use in their games.");
                EditorGUILayout.LabelField(" Open Source / And No GPL License (See License.txt)");
          

                EditorGUILayout.LabelField("Scale Amount: ");
                F4GDX2.scaleAmount = (Free4GameDevsX2.ScaleAmount)EditorGUILayout.EnumPopup(F4GDX2.scaleAmount);
  



                    EditorGUILayout.LabelField("Custom Input Folder? ");
                    F4GDX2.useCustomInputFolder = EditorGUILayout.Toggle(F4GDX2.useCustomInputFolder);

                if (F4GDX2.useCustomInputFolder == true)
                {
                    EditorGUILayout.LabelField("Input Folder (Relative to Assets ex. 'Scenes/InputFolder'): ");
                    F4GDX2.customInputFolder = EditorGUILayout.TextField(F4GDX2.customInputFolder);

                    if (GUILayout.Button("Select Folder"))
                    {
                        string path = EditorUtility.OpenFolderPanel("Select Input Folder", Application.dataPath, "");
                    if (path.IndexOf("/Assets/") == -1)
                    {
                        Debug.LogError("Please select a path relative to your assets folder!");
                    }
                    else
                    {
                        //Get relative Unity Assets project folder path....
                        string newPath = path.Substring(path.IndexOf("Assets/"), path.Length - path.IndexOf("Assets/"));
                        F4GDX2.customInputFolder = newPath;
                    }
                }
                }
      

                EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Scale!"))
                    {
                        F4GDX2.StartUp();
                    }
                EditorGUILayout.EndHorizontal();



                EditorGUILayout.EndVertical();

        }
    }

}