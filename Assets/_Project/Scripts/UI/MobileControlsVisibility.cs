using UnityEngine;

public class MobileControlsVisibility : MonoBehaviour
{
    private void Awake()
    {
// Hide if running in the Unity Editor, or if compiling for Standalone/WebGL builds

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }
}