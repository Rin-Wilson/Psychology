using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs input;
    public int occipitalLobe;
    public int frontalLobe;
    public int parietalLobe;
    public int temporalLobe;
    [Space(10)]
    public Transform head;

    private void Start()
    {
        input = GetComponent<StarterAssets.StarterAssetsInputs>();
    }
    void Update()
    {
        
    }

    private void interact()
    {
        if (input.Interact)
        {
            if (true)
            {

            }
        }
    }

    public int getOcptl()
    { return occipitalLobe; }

    public int getFrntl()
    { return frontalLobe; }

    public int getPrtl()
    { return parietalLobe; }

    public int getTmprl()
    { return temporalLobe; }
}
