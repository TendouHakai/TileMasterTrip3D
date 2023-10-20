using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class TileConfigEditor : EditorWindow
{
    public List<TileConfig> configs;

    [MenuItem("Window/GameData/TileConfig")]
    protected static void ShowWindow()
    {
        GetWindow<TileConfigEditor>("TileConfig");
    }

    private void OnEnable()
    {
        configs = TileConfigs.getInstance().getListConfigs();
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("List tile config: ");
        for (int row = 0; row < configs.Count; row++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label((row + 1).ToString(), GUILayout.Width(30));
            configs[row].ID = EditorGUILayout.IntField(configs[row].ID, GUILayout.Width(100));
            configs[row].img = EditorGUILayout.ObjectField(configs[row].img, typeof(Texture2D), false, GUILayout.Width(80), GUILayout.Height(80)) as Texture2D;

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                configs.RemoveAt(row);
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        if (GUILayout.Button("Add Tile config", GUILayout.Width(100)))
        {
            configs.Add(new TileConfig());
        }
        GUILayout.EndVertical();
    }

    //private void HandleDragAndDrop(Texture2D texture)
    //{
    //    Event currentEvent = Event.current;

    //    switch (currentEvent.type)
    //    {
    //        case EventType.DragUpdated:
    //        case EventType.DragPerform:
    //            if (!position.Contains(currentEvent.mousePosition))
    //                return;

    //            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

    //            if (currentEvent.type == EventType.DragPerform)
    //            {
    //                DragAndDrop.AcceptDrag();

    //                foreach (UnityEngine.Object draggedObject in DragAndDrop.objectReferences)
    //                {
    //                    if (draggedObject is Texture2D)
    //                    {
    //                        texture = (Texture2D)draggedObject;
    //                        Repaint(); // Refresh the window to update the displayed texture.
    //                        break; // You can choose what to do with multiple textures.
    //                    }
    //                }
    //            }
    //            break;
    //    }
    //}
}
