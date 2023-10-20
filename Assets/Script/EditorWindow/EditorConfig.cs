using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorConfig : EditorWindow
{
    protected GUIStyle tabStyleNormal;
    protected GUIStyle tabStyleSelected;
    protected GUIStyle tabStyleAddLevel;

    public void createGUIstyle()
    {
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
        tabStyleAddLevel.normal.background = MakeTexture(30, 30, new Color(0, 48f / 255, 48f / 255));
    }

    public Texture2D MakeTexture(int width, int height, Color color)
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

    public void DrawSprite(Texture2D sprite)
    {
        if (sprite != null)
        {
            //GUIContent content = new GUIContent(sprite);
            Rect rect = GUILayoutUtility.GetRect(80, 80, GUILayout.Width(80), GUILayout.Height(80)); // Kích thước cho mỗi sprite
            GUI.DrawTexture(rect, sprite, ScaleMode.ScaleToFit);
        }
    }
}
