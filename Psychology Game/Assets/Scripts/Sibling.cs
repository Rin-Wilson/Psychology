using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sibling : Item
{
    private UIManager ui;
    public GameObject popup;
    public TMPro.TextMeshProUGUI dialogueBox;
    public string[] lines;
    private int interactNum = 0;

    public override void TriggerInteract()
    {
        if(interactNum % 2 == 0)
        {
            ui.ShowElement(true, 1);
            ui.WriteDialogue("Fuck you lmao");
            ui.AddProgress(-200);
        }
        else
        {
            ui.ShowElement(false, 1);
        }

        interactNum++;
    }

    private void Start()
    {
        ui = GameObject.Find("/Canvas").GetComponent<UIManager>();
        interactNum = 0;
    }

    private void Update()
    {
        popup.SetActive(IsSelected());
        popup.transform.LookAt(GameObject.Find("/MainCamera").transform);
    }
}
