using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace UEditor{

public class WindowMenu : MonoBehaviour
{

#if UEditor_W0
    /// <summary>
    /// 编辑器标题栏添加/移除工程路径
    /// </summary>
    [MenuItem("Window/Show Project Path in Title")]
    static void AddProjectPathInTitle(){
        AddProjectPathToWindowTitle();
    }
#endif

    public static void AddProjectPathToWindowTitle() {
        var handler = User32API.GetCurrentWindowHandle();
        string name = User32API.GetWindowTitle(handler);

        string path = Directory.GetCurrentDirectory();

        if (name.Contains(path))
            return;

        User32API.SetWindowText(handler, name + " - " + path);
    }

    public static void RemoveProjectPathToWindowTitle() {
        var handler = User32API.GetCurrentWindowHandle();
        string name = User32API.GetWindowTitle(handler);

        string path = Directory.GetCurrentDirectory();

        User32API.SetWindowText(handler, name.Replace(" - " + path, ""));
    }

}

}
