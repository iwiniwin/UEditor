using UnityEngine;
using UnityEditor;
using System.IO;

namespace UEditor {

public class FileMenuExtend
{
#if UEditor_F0
    [MenuItem("File/Open Editor Folder", false, 0)]
    static void OpenEditorFolder(){
        string editorPath = Path.GetDirectoryName(EditorApplication.applicationContentsPath);
        Application.OpenURL("file://" + editorPath);
    }
#endif

#if UEditor_F1
    [MenuItem("File/Open Project Folder", false, 0)]
    static void OpenProjectFolder(){
        string projectPath = Path.GetDirectoryName(Application.dataPath);
        Application.OpenURL("file://" + projectPath);
    }
#endif
}

}
