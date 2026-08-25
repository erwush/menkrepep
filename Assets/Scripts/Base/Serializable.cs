using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Description
{

    [TextArea(3, 10)] public string description;
    public List<ExplainData> explanation = new List<ExplainData>();

}
