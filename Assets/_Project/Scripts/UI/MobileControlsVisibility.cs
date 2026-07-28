using UnityEngine;

// Hides the entire mobile control scheme on non-touch platforms so
// desktop/WebGL builds aren't cluttered with on-screen buttons meant
// only for Android/iOS.
public class MobileControlsVisibility : MonoBehaviour
{
    private void Start()
    {
        #if !UNITY_ANDROID && !UNITY_IOS
        gameObject.SetActive(false);
        #endif
    }
}