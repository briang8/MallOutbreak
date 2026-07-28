using UnityEngine;
using UnityEngine.EventSystems;

// Minimal on-screen virtual joystick for mobile input. Desktop/WebGL
// builds ignore this entirely and use keyboard input instead.
public class MobileInputProvider : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public static MobileInputProvider Instance { get; private set; }

    [SerializeField] private RectTransform joystickBackground;
    [SerializeField] private RectTransform joystickHandle;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Joystick: OnBeginDrag firing");
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Joystick: OnDrag firing");

        Vector2 direction = eventData.position - (Vector2)joystickBackground.position;
        direction = Vector2.ClampMagnitude(direction, joystickBackground.sizeDelta.x / 2);
        joystickHandle.anchoredPosition = direction;

        Horizontal = direction.x / (joystickBackground.sizeDelta.x / 2);
        Vertical = direction.y / (joystickBackground.sizeDelta.x / 2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Joystick: OnPointerDown firing");
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Horizontal = 0f;
        Vertical = 0f;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}