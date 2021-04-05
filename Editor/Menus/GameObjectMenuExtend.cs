using UnityEditor;
using UnityEngine;

namespace UEditor
{
    public class GameObjectMenuExtend
    {
#if UEditor_G0
    /// <summary>
    /// 复制场景中游戏对象的相对路径
    /// </summary>
    [MenuItem("GameObject/Copy Relative Path", false, 20)]
    static void CopyRelativePath(){
        UnityEngine.Object obj = Selection.activeObject;
        if(obj == null) return;
        string result = AssetDatabase.GetAssetPath(obj);
        if(string.IsNullOrEmpty(result)) {
            Transform selectChild = Selection.activeTransform;
            if(selectChild != null) {
                result = selectChild.name;
                while(selectChild.parent != null) {
                    selectChild = selectChild.parent;
                    result = string.Format("{0}/{1}", selectChild.name, result);
                }
            }
        }
        // 复制到剪贴板
        TextEditor editor = new TextEditor();
        editor.content = new GUIContent(result);
        editor.OnFocus();
        editor.Copy();
    }
#endif
    }
}


