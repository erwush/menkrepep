using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class UiManager : MonoBehaviour
{


    public static UiManager Instance;
    public TurnManager turn = TurnManager.Instance;
    public GameObject displayPanel;

    public Dictionary<string, bool> displayUi = new Dictionary<string, bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayUi.Add("isRotated", false);
        displayUi.Add("isOpen", true);

    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void RotateUi()
    {
        Animator anim = turn.activePlayer.displayPanel.GetComponent<Animator>();
        anim.speed = 1f;
        if (displayUi["isRotated"]) anim.Play("ReverseRotate");
        else anim.Play("Rotate");

        StartCoroutine(RotatingUi());
    }

    public IEnumerator RotatingUi()
    {
        Animator anim = turn.activePlayer.displayPanel.GetComponent<Animator>();
        yield return new WaitForSeconds(0.5f);
        anim.speed = 0f;
        if (displayUi["isRotated"]) displayPanel.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        else displayPanel.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 180, 0);
        if (displayUi["isRotated"]) displayUi["isRotated"] = false;
        else displayUi["isRotated"] = true;
    }

    public void ToggleUi()
    {
        Animator anim = turn.activePlayer.displayPanel.GetComponent<Animator>();
        if (displayUi["isRotated"]) anim.Play("Close");
        else anim.Play("Open");

    }
}
