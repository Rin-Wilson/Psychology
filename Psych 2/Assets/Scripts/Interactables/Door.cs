using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool open;
    public float openRotation;
    public float closedRotation;
    [Header("Audio")]
    public AudioSource openSFX;
    public AudioSource closedSFX;
    private float targetRotation;
    private float interpol;

    private void Update()
    {
        RotateDoor();
    }

    public override void triggerInteract()
    {
        open = !open;
        if (open)
        {
            openSFX.Play();
        }
        else
        {
            closedSFX.Play();
        }
    }

    private void RotateDoor()
    {
        targetRotation = open ? 1 : 0;

        if (interpol > targetRotation + 0.01 || interpol < targetRotation - 0.01)
        {
            interpol = Mathf.Lerp(interpol, targetRotation, 1f * Time.deltaTime);
        }
        else
        {
            interpol = targetRotation;
        }

        float targetOpen = openRotation - closedRotation;

        transform.rotation = Quaternion.Euler(new Vector3(0f, closedRotation + (targetOpen * interpol), 0f));
    }
}