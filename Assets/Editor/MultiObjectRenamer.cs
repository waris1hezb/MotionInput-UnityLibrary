using UnityEditor;
using UnityEngine;

public class MultiObjectRenamer : EditorWindow
{
    private string findString = "Hover";
    private string replaceString = "Focus";

    [MenuItem("Tools/Multi Object Renamer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MultiObjectRenamer));
    }

    private void OnGUI()
    {
        GUILayout.Label("Multi Object Renamer", EditorStyles.boldLabel);

        findString = EditorGUILayout.TextField("Find", findString);
        replaceString = EditorGUILayout.TextField("Replace", replaceString);

        if (GUILayout.Button("Rename"))
        {
            RenameSelectedObjects();
        }
    }

    private void RenameSelectedObjects()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            string newName = obj.name.Replace(findString, replaceString);
            Undo.RecordObject(obj, "Rename Object");
            obj.name = newName;
        }
    }
}
