using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs inputs;
    private List<GameObject> nearbyItems;
    private Item nearestItem;
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    //private StarterAssets.ThirdPersonController controller;

    [Header("Stats")]
    public int iqPoints;
    [Space(10)]
    public int frontal;
    public int occipital;
    public int temporal;
    public int parietal;
    [Space(10)]
    [Header("stat effects")]
    public int startingFov = 10;


    void Start()
    {
        inputs = GetComponent<StarterAssets.StarterAssetsInputs>();
        nearbyItems = new List<GameObject>();
        virtualCamera = GameObject.Find("/PlayerFollowCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        //controller = GetComponent<StarterAssets.ThirdPersonController>();
        virtualCamera.m_Lens.FieldOfView = startingFov;
    }

    void Update()
    {
        FindClosest();
        HandleInteraction();
        ManageUI();
    }

    public void addIQ(int amount)
    {
        iqPoints += amount;
    }

    public void AddOccipital()
    {
        if(iqPoints > 0)
        {
            occipital++;
            virtualCamera.m_Lens.FieldOfView = occipital + startingFov;
            iqPoints--;
        }
    }

    public void AddFrontal()
    {
        if(iqPoints > 0)
        {
            frontal++;
            iqPoints--;
        }
    }

    public void AddTemporal()
    {
        if (iqPoints > 0)
        {
            temporal++;
            iqPoints--;
        }
    }

    public void AddParietal()
    {
        if(iqPoints > 0)
        {
            parietal++;
            iqPoints--;
        }
    }

    //UI Handling
    private void ManageUI()
    {
        UIManager ui = GameObject.Find("/Canvas").GetComponent<UIManager>();
        ui.SetIq(iqPoints);

        ui.SetOccipital(occipital);
        ui.SetFrontal(frontal);
        ui.SetParietal(parietal);
        ui.SetTemporal(temporal);
        
        if (inputs.upgradeMenu)
        {
            ui.ShowUpgradeMenu(true);
            inputs.cursorInputForLook = false;
            inputs.cursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            ui.ShowUpgradeMenu(false);
            inputs.cursorInputForLook = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    //Item Handling
    private void HandleInteraction()
    {
        if (inputs.interact)
        {
            if (nearestItem != null)
            {
                nearestItem.TriggerInteract();
            }
        }
    }

    private void FindClosest()
    {
        if(nearbyItems.Count > 0)
        {
            GameObject nearest = nearbyItems[0];

            if(nearest == null)
            {
                nearbyItems.Remove(nearest);
                return;
            }
            foreach (var item in nearbyItems)
            {
                float nearestDist = Vector3.Distance(nearest.transform.position, transform.position);
                float itemDist = Vector3.Distance(item.transform.position, transform.position);

                if (itemDist < nearestDist)
                {
                    nearest = item;
                }
            }

            if (nearestItem != null)
            nearestItem.Deselect();

            nearestItem = nearest.GetComponent<Item>();
            nearestItem.Select();
        }
        else
        {
            nearestItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item i = other.GetComponent<Item>();
        if (i != null)
        {
            if (!i.GetIsSeen())
            {
                i.SetIsSeen(true);
                nearbyItems.Add(i.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Item i = other.GetComponent<Item>();
        if (i != null)
        {
            nearbyItems.Remove(i.gameObject);
            i.SetIsSeen(false);
            if (i.IsSelected())
            {
                i.Deselect();
            }
        }
    }
}
