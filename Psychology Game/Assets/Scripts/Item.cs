using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;

    private bool isSeen;
    private bool isSelected;

    private Player player;

    private void Start()
    {
        player = GameObject.Find("/PlayerArmature").GetComponent<Player>();
    }

    public virtual void TriggerInteract()
    {
        return;
    }

    public virtual void Select()
    {
        isSelected = true;
    }

    public virtual void Deselect()
    {
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public bool GetIsSeen() { return isSeen; }
    public void SetIsSeen(bool seen) { isSeen = seen; }

}
