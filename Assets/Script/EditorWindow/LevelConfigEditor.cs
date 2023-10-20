using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class LevelConfigEditor : EditorConfig
{
    int tabIndex = 0;
    public List<LevelConfig> configs;

    // comboBox
    public List<string> options = new List<string>();  
    public List<TileConfig> tileConfigs;

    [MenuItem("Window/GameData/LevelConfig")]
    protected static void ShowWindow()
    {
        GetWindow<LevelConfigEditor>("LevelConfig");
    }

    private void OnEnable()
    {
        configs = LevelConfigs.getInstance().getListConfigs();

        tileConfigs = TileConfigs.getInstance().getListConfigs();
        options.Clear();
        foreach (TileConfig tileConfig in tileConfigs)
        {
            options.Add(tileConfig.ID.ToString());
        }
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical();
        createGUIstyle();

        drawLevelTab();
        GUILayout.EndVertical();
        DrawContent();
    }

    public void drawLevelTab()
    {
        int buttonsPerRow = (int)(position.width / tabStyleNormal.fixedWidth);

        GUILayout.BeginVertical("Box");
        for (int i = 0; i <= configs.Count; i++)
        {
            if (i % buttonsPerRow == 0)
            {
                EditorGUILayout.BeginHorizontal();
            }

            if(i<configs.Count-1)
            {
                bool isSelected = (i == tabIndex);
                GUIStyle style = isSelected ? tabStyleSelected : tabStyleNormal;
                if (GUILayout.Button(configs[i].Name, style))
                {
                    tabIndex = i;
                }
            }
            else if(i == configs.Count)
            {
                if (GUILayout.Button("+", tabStyleAddLevel))
                {
                    configs.Add(new LevelConfig());
                    tabIndex = configs.Count - 1;
                }
            }

            if ((i + 1) % buttonsPerRow == 0 || i==configs.Count)
            {
                // Kết thúc dòng
                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.EndVertical();
    }

    public void DrawContent()
    {
        if (configs.Count <= 0) return;
        configs[tabIndex].ID = EditorGUILayout.IntField("IDLevel", configs[tabIndex].ID, GUILayout.MaxWidth(500));
        configs[tabIndex].Name = EditorGUILayout.TextField("Name Level", configs[tabIndex].Name, GUILayout.MaxWidth(500));
        configs[tabIndex].Time = EditorGUILayout.IntField("Play time", configs[tabIndex].Time, GUILayout.MaxWidth(500));
        if(GUILayout.Button("Delete this Level", GUILayout.Width(150)))
        {
            configs.RemoveAt(tabIndex);
            if(configs.Count <= 0)
            {
                return;
            }
            if (tabIndex >= configs.Count)
            {
                --tabIndex;
            }
        }

        GUILayout.Space(20);
        GUILayout.Label("List tile in this Map: ");
        // Đầu tiên, chúng ta có thể vẽ tiêu đề của từng cột
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Num", GUILayout.Width(30));
        GUILayout.Label("TileID", GUILayout.Width(100));
        GUILayout.Label("Sprite", GUILayout.Width(80));
        GUILayout.Label("Count", GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();
        
        // Sau đó, chúng ta vẽ nội dung của bảng
        for (int row = 0; row < configs[tabIndex].tileInLevels.Count; row++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label((row+1).ToString(), GUILayout.Width(30));
            configs[tabIndex].tileInLevels[row].IDTile = EditorGUILayout.Popup(configs[tabIndex].tileInLevels[row].IDTile, options.ToArray(), GUILayout.Width(100));
            DrawSprite(tileConfigs[configs[tabIndex].tileInLevels[row].IDTile].img);
            configs[tabIndex].tileInLevels[row].count = EditorGUILayout.IntField(configs[tabIndex].tileInLevels[row].count, GUILayout.Width(100));
            
            if(configs[tabIndex].tileInLevels.Count > 1)
            {
                if (row == 0)
                {
                    if (GUILayout.Button("↓", GUILayout.Width(40)))
                    {
                        Swap(0, 1);
                    }
                }
                else if (row == configs[tabIndex].tileInLevels.Count - 1)
                {
                    if (GUILayout.Button("↑", GUILayout.Width(40)))
                    {
                        Swap(row, row -1);
                    }
                }
                else
                {
                    if (GUILayout.Button("↓", GUILayout.Width(20)))
                    {
                        Swap(row, row+1);
                    }

                    if (GUILayout.Button("↑", GUILayout.Width(20)))
                    {
                        Swap(row, row-1);
                    }
                }
            }

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                configs[tabIndex].tileInLevels.RemoveAt(row);
            }
            
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
        }

        if(GUILayout.Button("Add Tiles", GUILayout.Width(100)))
        {
            configs[tabIndex].tileInLevels.Add(new TileInLevel(Int32.Parse(options[0])));
        }
    }

    public void Swap(int i, int j)
    {
        TileInLevel temp = configs[tabIndex].tileInLevels[i];
        configs[tabIndex].tileInLevels[i] = configs[tabIndex].tileInLevels[j];
        configs[tabIndex].tileInLevels[j] = temp;
    }
}