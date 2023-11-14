using UnityEditor;
using UnityEngine;

public class BasePlateInstantiator : EditorWindow
{
    private int gridSize = 5; // Change this to the desired grid size (n)
    private float xOffset = 1.0f; // Change this to the desired X offset
    private float zOffset = 1.0f; // Change this to the desired Z offset
    private GameObject prefab; // Drag and drop the prefab in the Unity Editor

    [MenuItem("Tools/Base Plate Instantiator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BasePlateInstantiator), false, "BasePlateInstantiator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);

        gridSize = EditorGUILayout.IntField("Grid Size (n)", gridSize);
        xOffset = EditorGUILayout.FloatField("X Offset", xOffset);
        zOffset = EditorGUILayout.FloatField("Z Offset", zOffset);

        prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Instantiate Grid"))
        {
            InstantiateGrid();
        }
    }

    private void InstantiateGrid()
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab is not set!");
            return;
        }

        Transform parentObject = Selection.activeTransform;

        if (parentObject == null)
        {
            Debug.LogWarning("No object selected in the scene. Please select a GameObject to use as parent.");
            return;
        }

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 position = parentObject.position + new Vector3(i * xOffset, 0, j * zOffset);
                Instantiate(prefab, position, Quaternion.identity, parentObject);
            }
        }
    }
}
