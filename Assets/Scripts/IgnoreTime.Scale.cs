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

    // Se llama cuando el cursor entra en el bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = highlightedSprite;
    }

    // Se llama cuando el cursor sale del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }
}
