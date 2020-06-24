using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UEditor {

#if UEditor_C0
[InitializeOnLoad]
public class CompilerOptions 
{
    static CompilerOptions(){
        // EditorApplication.update += OnEditorUpdate;
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    }

    static void OnEditorUpdate(){
        if(EditorApplication.isCompiling) {
            EditorUtility.DisplayCancelableProgressBar("Script Compiling", "please wait for a moment ......", 1.0f);
        }
        if(EditorUtility.scriptCompilationFailed){
            EditorUtility.ClearProgressBar();
        }
    }

    // [UnityEditor.Callbacks.DidReloadScripts]
    // static void OnReloadScripts(){
    //     EditorUtility.ClearProgressBar();
    // }

    static void OnBeforeAssemblyReload() {
        EditorUtility.DisplayProgressBar("Script Compiling", "please wait for a moment ......", 1.0f);
    }

    static void OnAfterAssemblyReload() {
        EditorUtility.ClearProgressBar();
    }
}
#endif

}
