using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jared : Interactable
{
    public GameObject popup;
    public HUD ui;

    public string[] randomFacts;
    private int currentConversation;
    public int conversationStage;

    public float delayCounter;
    public bool countDelay;
    private bool isInteracting;
    private GameObject mainCamera;
    public bool waiting;
    private bool alreadyBeen;
    private bool heardFact;

    private UnityEvent[] currentList;
    public UnityEvent[] conversationList;
    public UnityEvent[] conversationListop1;
    public UnityEvent[] conversationListop2;
    public UnityEvent[] conversationListop3;
    public UnityEvent[] conversationListop4;
    [Header("Sublists")]
    public UnityEvent[] subList1;
    public UnityEvent[] subList2;
    public UnityEvent[] subList3;
    public UnityEvent[] subList4;
    private void Start()
    {
        mainCamera = GameObject.Find("/Main Camera");
        conversationStage = -1;
        currentList = conversationList;
    }
    public override void triggerInteract()
    {
        if (!isInteracting)
        {
            conversationStage = -1;
            currentList = conversationList;
        }
        if(conversationStage < currentList.Length - 1 && !countDelay && !waiting)
        {
            conversationStage++;
            currentList[conversationStage].Invoke();
        }
    }

    private void Update()
    {
        popup.SetActive(isSelected);
        popup.transform.LookAt(mainCamera.transform);

        if (waiting && !countDelay)
        {
            if(ui.promptResponse != 0)
            {
                conversationStage += ui.promptResponse;
                currentList[conversationStage].Invoke();
                ui.promptResponse = 0;
                waiting = false;
            }
        }

        if (delayCounter >= 0)
        {
            delayCounter -= Time.deltaTime;
        }
        else
        {
            if (conversationStage < currentList.Length && isInteracting && countDelay)
            {
                conversationStage++;
                currentList[conversationStage].Invoke();
            }
            delayCounter = 1;
            countDelay = false;
        }
    }

    public void TaskEnlightenment()
    {
        if(!heardFact)
        ui.NewTask("Enlightenment", "Maybe (Sibling) has something really cool to tell you?");
    }

    public void TaskTellParental()
    {
        ui.NewTask("Tell (Parental Unit)", "(Parental Unit) Should know this too! (Parental Unit) can be found in the study.");
    }

    public void TaskFactCheck()
    {
        ui.NewTask("Fact Check", "Seems a little fishy... look it up on a [computer] or go ask (Parental Unit). (Parental Unit) Can be found In the study. [Computer]s are upstairs.");
    }

    private string PickRandomFact()
    {

        int random = Random.Range(0, randomFacts.Length);

        return randomFacts[random];
    }

    public void Delay(float seconds)
    {
        delayCounter = seconds;
        countDelay = true;
    }

    public void setInteractionState(bool state)
    {
        isInteracting = state;
    }

    public void WaitForResponse()
    {
        ui.promptResponse = 0;
        waiting = true;
    }

    public void startList(int list)
    {
        conversationStage = 0;
        if(list == 0)
        {
            currentList = conversationList;
        }
        if(list == 1)
        {
            currentList = conversationListop1;
        }
        if(list == 2)
        {
            currentList = conversationListop2;
        }
        if(list == 3)
        {
            currentList = conversationListop3;
        }
        if(list == 4)
        {
            currentList = conversationListop4;
        }
        if(list == 5)
        {
            currentList = subList1;
        }
        if(list == 6)
        {
            currentList = subList2;
        }
        if(list == 7)
        {
            currentList = subList3;
        }
        if(list == 8)
        {
            currentList = subList4;
        }
        currentList[conversationStage].Invoke();
    }

    public void StartListAtStage(int stage)
    {
        conversationStage = stage;
        currentList = conversationList;
        currentList[conversationStage].Invoke();
    }

    public void StartMessage()
    {
        if (alreadyBeen)
        {
            if (heardFact)
            {
                ui.WriteDialogue("Do you want to hear another cool (and accurate) fact?");
            }
            else
            {
                ui.WriteDialogue("Wanna hear that cool fact now?");
            }
        }
        else
        {
            ui.WriteDialogue("Wanna hear a cool fact?");
            alreadyBeen = true;
        }
    }

    public void SayCoolFact()
    {
        string randomFact = PickRandomFact();
        ui.WriteDialogue(randomFact);
        heardFact = true;
        ui.storedMisinformation = randomFact;
    }
}
