using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ExplainSystem : MonoBehaviour
{

    public static ExplainSystem Instance;
    public List<ExplainData> activeExplain = new List<ExplainData>();
    public List<GameObject> activeObj = new List<GameObject>();
    public GameObject explainParent, explanation, explainUi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Instance = this;
    }

    public void ShowExplain()
    {
        explainUi.SetActive(true);
        foreach (var obj in activeObj)
        {
            Destroy(obj);
        }
        foreach (var explain in activeExplain)
        {
            GameObject obj = Instantiate(explanation, explainParent.transform);
            obj.GetComponent<Explanation>().Setup(explain);
            activeObj.Add(obj);
        }
    }
    
    public void CloseExplain()
    {
        explainUi.SetActive(false);
    }

}
