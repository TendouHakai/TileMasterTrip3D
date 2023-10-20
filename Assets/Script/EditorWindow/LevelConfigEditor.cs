using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class LevelConfigEditor : EditorWindow
{
    int tabIndex = 0;
    public List<LevelConfig> configs;

    private GUIStyle tabStyleNormal;
    private GUIStyle tabStyleSelected;
    private GUIStyle tabStyleAddLevel;

    // comboBox
    public List<string> options = new List<string>();  
    public List<TileConfig> tileConfigs;

    private Vector2 scrollPosition = Vector2.zero;

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
        // Tạo GUIStyle cho tab khi không được chọn
        tabStyleNormal = new GUIStyle(GUI.skin.button);
        tabStyleNormal.fixedHeight = 30;
        tabStyleNormal.fixedWidth = 80;

        // Tạo GUIStyle cho tab khi được chọn
        tabStyleSelected = new GUIStyle(GUI.skin.button);
        tabStyleSelected.fixedHeight = 30;
        tabStyleSelected.fixedWidth = 80;
        tabStyleSelected.normal.background = MakeTexture(80, 30, Color.black);

        // Tạo GUIStyle cho button add level
        tabStyleAddLevel = new GUIStyle(GUI.skin.button);
        tabStyleAddLevel.fixedHeight = 30;
        tabStyleAddLevel.fixedWidth = 30;
        tabStyleAddLevel.normal.background = MakeTexture(30, 30, new Color(0, 48f/255, 48f/255));

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

    private Texture2D MakeTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
    private void DrawSprite(Texture2D sprite)
    {
        if (sprite != null)
        {
            //GUIContent content = new GUIContent(sprite);
            Rect rect = GUILayoutUtility.GetRect(80, 80, GUILayout.Width(80), GUILayout.Height(80)); // Kích thước cho mỗi sprite
            GUI.DrawTexture(rect, sprite, ScaleMode.ScaleToFit);
        }
    }
}