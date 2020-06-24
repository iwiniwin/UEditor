/// <summary>
/// 将从指定位置观察到的场景图像存储到cubemap中
/// </summary>
using UnityEngine;
using UnityEditor;

namespace UEditor {

#if UEditor_GR0
public class RenderCubemapWizard : ScriptableWizard
{
    public Transform renderFromPosition;
    public Cubemap cubemap;

    private void OnWizardUpdate() {
        helpString = "Select transform to render from and cubemap to render into";
        isValid = (renderFromPosition != null && cubemap != null);
    }

    private void OnWizardCreate() {
        // 创建用于渲染的临时摄像机
        GameObject go = new GameObject("Cubemap Camera");
        go.AddComponent<Camera>();

        // 将摄像机放置到指定位置 
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = renderFromPosition.rotation;

        go.GetComponent<Camera>().RenderToCubemap(cubemap);

        DestroyImmediate(go);
    }

    [MenuItem("GameObject/Render into Cubemap")]
    static void RenderCubemap() {
        // 设置窗口title以及Render按钮
        ScriptableWizard.DisplayWizard<RenderCubemapWizard>("Render Cubemap", "Render");
    }
}
#endif

}
