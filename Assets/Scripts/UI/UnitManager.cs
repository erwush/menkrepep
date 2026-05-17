using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
