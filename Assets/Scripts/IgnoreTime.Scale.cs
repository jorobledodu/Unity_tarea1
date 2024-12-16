using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Se llama cuando el cursor entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = highlightedSprite;
    }

    // Se llama cuando el cursor sale del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }
}
