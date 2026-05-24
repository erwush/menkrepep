using UnityEngine;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;

    [SerializeField] private GameObject tooltip;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private RectTransform tooltipRect;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    void Update()
    {
        tooltipRect.position = new Vector3(Input.mousePosition.x+240, Input.mousePosition.y+40, 0);
    }

    public void Show(string text)
    {
        tooltip.SetActive(true);
        tooltipText.text = text;
    }

    public void Hide()
    {
        tooltip.SetActive(false);
    }

}
