using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Explanation : MonoBehaviour
{
    public ExplainData data;
    public TextMeshProUGUI explainName, explainDesc;

    public void Setup(ExplainData data)
    {
        this.data = data;
        explainName.text = data.explainName;
        explainDesc.text = data.explainDesc;
    }
    
}
