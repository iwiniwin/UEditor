using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainWindow : EditorWindow
{
    static string[,] Features = new string[,]{
        // 描述，宏
        {"清除无用资源（Assets/Extend/Clear）", "UEditor_A0"},
        {"查找重复资源（Assets/Extend/Find Duplicate Resources）", "UEditor_A1"},
    };

    static bool[] options = new bool[Features.GetLength(0)];

    static HashSet<string> macroSet = null;

    [MenuItem("Window/Editor Window")]
    static void InitMainWindow(){
        string macroStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
        string[] macros = macroStr.Split(';');
        macroSet = new HashSet<string>(new List<string>(macros));
        for(int i = 0; i < options.Length; i ++){
            if(macroSet.Contains(Features[i, 1]))
                options[i] = true;
            else
                options[i] = false;
        }
        EditorWindow.GetWindow(typeof(MainWindow));;
    }

    Vector2 scrollPos;
    int bottomAreaWidth = 25;
    int buttonGap = 5;

    void OnGUI(){

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));

        for(int i = 0; i < options.Length; i ++){
            options[i] = GetToggle(Features[i, 0], options[i]);
        }

        GUILayout.BeginArea( new Rect(buttonGap, position.height - bottomAreaWidth, position.width - buttonGap * 2, position.height));

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("取消")){
            this.Close();
        }
        if(GUILayout.Button("保存")){
            Save();
            this.Close();
        }
        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();

        EditorGUILayout.EndScrollView();
        
    }

    bool GetToggle(string text, bool toggle){
        GUILayout.BeginHorizontal();
        GUILayout.Label(text);
        GUILayout.FlexibleSpace();
        toggle = GUILayout.Toggle(toggle, "");
        GUILayout.EndHorizontal();
        return toggle;
    }

    void Save(){
        for(int i = 0; i < options.Length; i ++){
            string macro = Features[i, 1];
            if(options[i] == true){
                macroSet.Add(macro);
            }else if(macroSet.Contains(macro)){
                macroSet.Remove(macro);
            }
        }
        string macroStr = string.Join(";", new List<string>(macroSet));
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, macroStr);
    }
}
