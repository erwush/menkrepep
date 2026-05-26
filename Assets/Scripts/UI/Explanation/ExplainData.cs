using UnityEngine;

[CreateAssetMenu(fileName = "ExplainData", menuName = "Explanation")]
public class ExplainData : ScriptableObject
{

    public string explainName;
    [TextArea(3,10)] public string explainDesc;

}
