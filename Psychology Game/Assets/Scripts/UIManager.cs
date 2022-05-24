using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    //References to the text fields
    public TMPro.TextMeshProUGUI iqScore;

    public TMPro.TextMeshProUGUI occipitalScore;
    public TMPro.TextMeshProUGUI frontalScore;
    public TMPro.TextMeshProUGUI temporalScore;
    public TMPro.TextMeshProUGUI parietalScore;
    public TMPro.TextMeshProUGUI dialogueBox;
    public AudioSource dialogueClick;

    public GameObject[] elements;
    private StarterAssets.StarterAssetsInputs inputs;
    public float textCounter;
    public float textSpeed;
    private int nextLetter;

    private string dialogue;

    [Space(10)]
    public Progress progressBar;
    public UnityEvent onWin;
    public UnityEvent onFail;
    void Start()
    {
        inputs = GameObject.Find("/PlayerArmature").GetComponent<StarterAssets.StarterAssetsInputs>();
        ClearDialogue();
    }

    void Update()
    {
        progressBar.updateProgress();
        if (progressBar.winProgress >= 500f)
        {
            onWin.Invoke();
        }
        if(progressBar.failProgress >= 500f)
        {
            onFail.Invoke();
        }

        bool isOpen = false;
        foreach (GameObject g in elements)
        {
            if (g.activeInHierarchy)
            {
                isOpen = true;
            }
        }

        MouseControl(isOpen);
        DialogueCounter();
    }

    private void DialogueCounter()
    {
        textCounter = textCounter > textSpeed ? 0f : textCounter + Time.deltaTime;

        if (textCounter >= textSpeed)
        {
            if(dialogueBox.text != dialogue)
            {
                dialogueBox.text += dialogue.Substring(nextLetter, 1);
                
                if(nextLetter < dialogue.Length - 1)
                {
                    dialogueClick.Play();
                    nextLetter++;
                }
            }
        }
    }

    public void AddProgress(float addedProgress)
    {
        if(addedProgress < 0)
        {
            progressBar.failProgress -= addedProgress;
        }
        else
        {
            progressBar.winProgress += addedProgress;
        }
    }

    public void WriteDialogue(string line)
    {
        dialogueBox.text = "";
        nextLetter = 0;
        dialogue = line;
    }

    public void ClearDialogue()
    {
        dialogueBox.text = "";
        dialogue = "";
    }

    public void MouseControl(bool state)
    {
        inputs.cursorInputForLook = !state;
        inputs.lockMovement = !state;
        Cursor.lockState = !state? CursorLockMode.Locked : CursorLockMode.None;
    }

    //set iq display
    public void SetIq(int iq)
    {
        iqScore.text = "" + iq;
    }

    //Set the lobe displays
    public void SetOccipital(int score)
    {
        occipitalScore.text = "Occipital [" + score + "]";
    }

    public void SetFrontal(int score)
    {
        frontalScore.text = "Frontal [" + score + "]";
    }

    public void SetTemporal(int score)
    {
        temporalScore.text = "Temporal [" + score + "]";
    }

    public void SetParietal(int score)
    {
        parietalScore.text = "Parietal [" + score + "]";
    }

    public void ShowElement(bool show, int index)
    {
        elements[index].SetActive(show);
    }
}

[System.Serializable]
public class Progress
{
    public RectTransform winBar;
    public RectTransform failBar;
    public float winProgress;
    public float failProgress;

    private float visualProgress_W;
    private float visualProgress_F;

    public void updateProgress()
    {
        if (visualProgress_W < winProgress - 0.01 || visualProgress_W > winProgress + 0.01)
        {
            visualProgress_W = Mathf.Lerp(visualProgress_W, winProgress, 2f * Time.deltaTime);
        }
        else
        {
            visualProgress_W = winProgress;
        }

        if (visualProgress_F < failProgress - 0.01 || visualProgress_F > failProgress + 0.01)
        {
            visualProgress_F = Mathf.Lerp(visualProgress_F, failProgress, 2f * Time.deltaTime);
        }
        else
        {
            visualProgress_F = failProgress;
        }

        winBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualProgress_W);
        failBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualProgress_F);
    }
}
