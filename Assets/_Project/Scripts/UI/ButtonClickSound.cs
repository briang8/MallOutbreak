using UnityEngine;
using UnityEngine.UI;

// used on any Button to make it play the standard UI click sound.
// Keeps audio wiring out of every individual button-handler method.

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlayUiClick());
    }
}