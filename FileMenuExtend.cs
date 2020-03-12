using UnityEngine;
using UnityEditor;
using System.IO;
using static UKit.Utils.Output;

public class FileMenuExtend : MonoBehaviour
{
    [MenuItem("File/Open Editor Folder", false, 0)]
    static void OpenEditorFolder(){
        string editorPath = Path.GetDirectoryName(EditorApplication.applicationContentsPath);
        Application.OpenURL("file://" + editorPath);
    }

    [MenuItem("File/Open Project Folder", false, 0)]
    static void OpenProjectFolder(){
        string projectPath = Path.GetDirectoryName(Application.dataPath);
        Application.OpenURL("file://" + projectPath);
    }
}
