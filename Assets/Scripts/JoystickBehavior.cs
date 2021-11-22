using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class JoystickBehavior : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image JoyBG;
    [SerializeField]
    private Image Joystick;
    private Vector2 inputVector;
    public void Start()
    {
        JoyBG = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        Joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoyBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoyBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / JoyBG.rectTransform.sizeDelta.x);
            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            Joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (JoyBG.rectTransform.sizeDelta.x / 2), inputVector.y * (JoyBG.rectTransform.sizeDelta.y / 2));
        }
    }
    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }

}
