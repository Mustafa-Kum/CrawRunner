using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JumpButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.player.JumpButton();
    }
}
