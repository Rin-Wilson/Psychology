using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : Item
{
    public GameObject popup;
    public override void TriggerInteract()
    {
        Debug.Log("Interaction complete" + gameObject.name);
        GetPlayer().addIQ(4);
        Destroy(gameObject);
    }

    private void Update()
    {
        popup.SetActive(IsSelected());
        popup.transform.LookAt(GameObject.Find("/MainCamera").transform);
    }
}
