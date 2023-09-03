using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SlideButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.player.SlideButton();
    }
}
