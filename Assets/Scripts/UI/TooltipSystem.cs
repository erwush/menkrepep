using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;

    [SerializeField] private GameObject tooltip;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private RectTransform tooltipRect;
    [SerializeField] private Vector3 offset;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    void Update()
    {
        // tooltipRect.position = new Vector3(Input.mousePosition.x + 240, Input.mousePosition.y + 40, 0);
        Vector2 pos = Input.mousePosition + offset;

        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect);

        Vector2 size = tooltipRect.sizeDelta;

        pos.x = Mathf.Clamp(pos.x, 0, Screen.width - size.x);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height - size.y);

        tooltipRect.position = pos;
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
