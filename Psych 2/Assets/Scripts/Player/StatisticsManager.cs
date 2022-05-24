using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    [Header("Currency")]
    public int iq;
    [Header("Lobes")]
    public int frontalLobe;
    public int parietalLobe;
    public int temporalLobe;
    public int occipitalLobe;
    [Header("Effects")]
    public Cinemachine.CinemachineFreeLook mainCam;
    public float startingFOV = 30;

    private HUD ui;
    private void Start()
    {
        ui = GetComponent<HUD>();
    }

    private void Update()
    {
        UpdateUI();
        UpgradeEffects();
    }

    private void UpdateUI()
    {
        int[] scores =
        {
            iq,
            frontalLobe,
            parietalLobe,
            temporalLobe,
            occipitalLobe
        };

        ui.SetScores(scores);
    }

    private void UpgradeEffects()
    {
        mainCam.m_Lens.FieldOfView = startingFOV + occipitalLobe;
    }

    public void AddFrontal(int amount)
    {
        if(iq > 0)
        {
            frontalLobe += amount;
            iq -= amount;
        }
    }

    public void AddParietal(int amount)
    {
        if (iq > 0)
        {
            parietalLobe += amount;
            iq -= amount;
        }
    }

    public void AddTemporal(int amount)
    {
        if (iq > 0)
        {
            temporalLobe += amount;
            iq -= amount;
        }
    }

    public void AddOccipital(int amount)
    {
        if (iq > 0)
        {
            occipitalLobe += amount;
            iq -= amount;
        }
    }

    public void AddIQ(int amount)
    {
        iq += amount;
    }
}