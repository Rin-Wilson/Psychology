using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private InputManager inputs;
    public List<Interactable> nearbyItems;
    public Interactable nearestItem;
    private HUD ui;

    private void Start()
    {
        inputs = GetComponent<InputManager>();
        ui = GetComponent<HUD>();
    }

    private void Update()
    {
        FindClosest();

        ui.showHint = false;
        if (nearestItem !=null && inputs.interact)
        {
            nearestItem.triggerInteract();
            inputs.interact = false;
        }
        if (nearestItem != null)
        {
            ui.showHint = true;
            ui.SetHint("Press F to interact with " + nearestItem.interactableName);
        }
    }

    private void FindClosest()
    {
        float nearestDist = float.MaxValue;
        if(nearbyItems.Count > 0)
        {
            foreach (var item in nearbyItems)
            {
                if (Vector3.Distance(item.transform.position, transform.position) < nearestDist)
                {
                    nearestItem = item;
                    nearestDist = Vector3.Distance(item.transform.position, transform.position);
                }

                item.isSelected = nearestItem == item;
            }
        }
        else
        {
            nearestItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            nearbyItems.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
        {
            interactable.isSelected = false;
            nearbyItems.Remove(interactable);
        }
    }
}