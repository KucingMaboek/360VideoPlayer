using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 TouchDist { get; set; }
    private Vector2 pointerOld;
    private int pointerId;
    private bool pressed;

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[pointerId].position - pointerOld;
                pointerOld = Input.touches[pointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                pointerOld = Input.mousePosition;
            }

        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerId = eventData.pointerId;
        pointerOld = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
