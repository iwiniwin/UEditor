using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEditorInternal;
using UnityEditor.Scripting.ScriptCompilation;

public class MainWindow : EditorWindow
{
    static string[,] Features = new string[,]{
        // 描述，宏，修改后是否需要重启
        {"清除无用资源（Assets/Extend/Clear）", "UEditor_A0", ""},
        {"查找重复资源（Assets/Extend/Find Duplicate Resources）", "UEditor_A1", ""},
        {"根据自定义模板新建脚本并打开（Assets/Create/C# Stand Script）", "UEditor_A2", ""},

        {"打开编辑器所在目录（File/Open Editor Folder）", "UEditor_F0", ""},
        {"打开工程所在目录（File/Open Project Folder）", "UEditor_F1", ""},

        {"程序集重新加载时是否弹出提示", "UEditor_C0", "true"},
    };

    static bool[] options = new bool[Features.GetLength(0)];

    static HashSet<string> macroSet = null;

    [MenuItem("Window/UEditor Window")]
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
        EditorWindow.GetWindow(typeof(MainWindow), false, "UEditor Window").minSize = new Vector2(500, 200);
    }

    Vector2 scrollPos;
    int bottomAreaHeight = 24;
    int buttonGap = 5;

    void OnGUI(){

        GUIStyle style = new GUIStyle();
        style.fontSize = 15;
        style.padding = new RectOffset(5, 5, 5, 5);
        GUILayout.Label("启用/禁用以下编辑器扩展", style);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - bottomAreaHeight - buttonGap - 30));

        for(int i = 0; i < options.Length; i ++){
            options[i] = GetToggle(Features[i, 0], options[i]);
        }

        EditorGUILayout.EndScrollView();


        GUIStyle areaStyle = new GUIStyle();
        Color color;
        ColorUtility.TryParseHtmlString("#a2a2a2", out color);
        Texture2D texture = new Texture2D(128, 128);
        Color[] colors = texture.GetPixels();
        for(int i = 0; i < colors.Length; i ++)
            colors[i] = color;
        texture.SetPixels(colors);
        texture.Apply();
        areaStyle.normal.background = texture;
        GUILayout.BeginArea( new Rect(0, position.height - bottomAreaHeight, position.width, position.height), areaStyle);

        EditorGUILayout.BeginHorizontal();

        // GUIStyle tipStyle = new GUIStyle();
        // tipStyle.fontSize = 10;
        // tipStyle.padding = new RectOffset(5, 0, 6, 0);
        // Color tipColor;
        // ColorUtility.TryParseHtmlString("#434343", out tipColor);
        // tipStyle.normal.textColor = tipColor;
        // GUILayout.Label("tips：保存后请稍等片刻，编辑器重新编译后生效", tipStyle);

        GUILayout.FlexibleSpace();
        
        if(GUILayout.Button("保存", GUILayout.MaxWidth(100))){
            OnClickSave();
        }
        if(GUILayout.Button("取消", GUILayout.MaxWidth(100))){
            this.Close();
        }
        // GUILayout.FlexibleSpace();

        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();
    }

    bool GetToggle(string text, bool toggle){
        GUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle();
        Color color;
        ColorUtility.TryParseHtmlString("#434343", out color);
        style.normal.textColor = color;
        style.fontSize = 13;
        // left --
        style.padding = new RectOffset(10, 0, 0, 0);
        GUILayout.Label(text, style);
        GUILayout.FlexibleSpace();
        toggle = GUILayout.Toggle(toggle, "");
        GUILayout.EndHorizontal();
        return toggle;
    }

    void OnClickSave(){
        if(macroSet == null){
            this.ShowNotification(new GUIContent("数据异常无法保存，请关闭窗口重试"));
            return;
        }

        string restartTip = "";
        for(int i = 0; i < options.Length; i ++){
            if(Features[i, 2] == "true"){
                if(options[i] != macroSet.Contains(Features[i, 1])){
                    restartTip += ("\n\n    ● " + Features[i, 0]);
                }
            }
        }

        if(restartTip != ""){
            if(EditorUtility.DisplayDialog("提示", "修改了以下配置，需要重启才能保存" + restartTip + "\n\n立即保存重启编辑器？", "保存并重启", "取消")){
                this.Save();
                this.Close();
                Restart();
            }
        }else{
            int option = EditorUtility.DisplayDialogComplex("提示", "是否保存当前修改？\n\n保存后请稍等片刻，编辑器重新编译后生效", "保存", "取消", "保存并重启");
            switch(option){
                case 0:
                    this.Save();
                    this.Close();
                    break;
                case 1:
                    break;
                case 2:
                    this.Save();
                    this.Close();
                    Restart();
                    break;
            }
        }
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

    static void Reload(){
        Assembly assembly = Assembly.GetAssembly(typeof(Editor));
        System.Type editorUtilityType = assembly.GetType("UnityEditor.EditorUtility");
        editorUtilityType.GetMethod("RequestScriptReload", BindingFlags.Public | BindingFlags.Static);
    }

    static void Restart(){
        // 重启Editor
        Assembly assembly = Assembly.GetAssembly(typeof(Editor));
        System.Type editorApplicationType = assembly.GetType("UnityEditor.EditorApplication");
        editorApplicationType.GetMethod("RequestCloseAndRelaunchWithCurrentArguments", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
    }
}
