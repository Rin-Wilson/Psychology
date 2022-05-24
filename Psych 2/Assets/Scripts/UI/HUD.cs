using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    private InputManager inputs;
    public AudioManager audioManager;

    [Header("Elements")]
    public GameObject[] menuList;
    public RectTransform winBar;
    public RectTransform looseBar;
    public TMPro.TextMeshProUGUI dialogueBox;
    public TMPro.TextMeshProUGUI dialogueSpeaker;
    [Header("Scores")]
    public TMPro.TextMeshProUGUI[] scoreDisplays;

    [Header("Level Progress")]
    public float winProgress;
    public float looseProgress;
    private float visualWinProgress;
    private float visualLooseProgress;
    [Space(10)]
    public UnityEvent OnWin;
    public UnityEvent OnLoose;
    [Space(10)]
    [Header("Dialogue")]
    public float writeSpeed;
    public bool mouseOverride;

    public string storedMisinformation;
    [Header("Prompts")]
    public GameObject promptObject;
    public TMPro.TextMeshProUGUI op1;
    public TMPro.TextMeshProUGUI op2;
    public TMPro.TextMeshProUGUI op3;
    public TMPro.TextMeshProUGUI op4;

    public int promptResponse;
    [Header("Tasks")]
    public TMPro.TextMeshProUGUI taskTitle;
    public TMPro.TextMeshProUGUI taskInfo;
    [Header("Hints")]
    public TMPro.TextMeshProUGUI hint;
    public GameObject hintObject;
    public bool showHint;
    [Header("UI Animations")]
    public Animator uiAnimator;

    private float writeSpeedDelta;
    private string currentDialogue = "";
    private int nextLetter;

    private void Start()
    {
        inputs = GetComponent<InputManager>();
        foreach(GameObject menu in menuList)
        {
            menu.SetActive(false);
        }

        NewTask("Talk to (Sibling)", "(Sibling) Usually hangs out in the living room.");
    }

    public void WriteDialogue(string dialogue)
    {
        dialogueBox.text = "";
        currentDialogue = dialogue;
        nextLetter = 0;
    }

    public void SetSpeaker(string speaker)
    {
        dialogueSpeaker.text = speaker + ":";
    }
    
    private void UpdateDialogue()
    {
        writeSpeedDelta -= Time.deltaTime;
        if (writeSpeedDelta <= 0.0f)
        {
            writeSpeedDelta = writeSpeed;

            if (dialogueBox.text != currentDialogue)
            {
                dialogueBox.text += currentDialogue.Substring(nextLetter, 1);
                audioManager.click1.Play();
                if (nextLetter < currentDialogue.Length - 1)
                {
                    nextLetter++;
                }
            }
        }
    }

    private void Update()
    {
        MenuControl();
        MenuInputs();
        ProgressManagement();
        UpdateDialogue();
        AnimateUI();
    }

    private void ProgressManagement()
    {
        if (visualWinProgress < winProgress - 0.01 || visualWinProgress > winProgress + 0.01)
        {
            visualWinProgress = Mathf.Lerp(visualWinProgress, winProgress, 2f * Time.deltaTime);
        }
        else
        {
            visualWinProgress = winProgress;
        }

        if (visualLooseProgress < looseProgress - 0.01 || visualLooseProgress > looseProgress + 0.01)
        {
            visualLooseProgress = Mathf.Lerp(visualLooseProgress, looseProgress, 2f * Time.deltaTime);
        }
        else
        {
            visualLooseProgress = looseProgress;
        }

        winBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualWinProgress);
        looseBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualLooseProgress);

        if(winProgress >= 500)
        {
            OnWin.Invoke();
        }
        if (looseProgress >= 500)
        {
            OnLoose.Invoke();
        }
    }

    public void EnableMouse(bool allowed)
    {
        Cursor.lockState = !allowed ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void SetScores(int[] scores)
    {
        for (int i = 0; i < scoreDisplays.Length; i++)
        {
            if (scoreDisplays[i] != null)
                scoreDisplays[i].text = scores[i].ToString();
        }
    }

    public void AddWin(float amount)
    {
        winProgress += amount;
    }

    public void AddLoose(float amount)
    {
        looseProgress += amount;
    }

    private void MenuControl()
    {
        bool open = false;
        foreach (GameObject menu in menuList)
        {
            if (menu.activeInHierarchy)
            {
                open = true;
            }
        }

        EnableMouse(open);
        inputs.allowMove = !open;
        inputs.allowLook = !open;
        audioManager.mainBackGroundMusic.volume = open ? audioManager.bgmMenuVolume : audioManager.bgmVolume;
        hintObject.SetActive(!open);
    }

    private void MenuInputs()
    {
        if (inputs.upgradeMenu)
        {
            menuList[0].SetActive(!menuList[0].activeInHierarchy);
            inputs.upgradeMenu = false;
        }
    }

    public void ShowDialogue(bool shown)
    {
        menuList[1].SetActive(shown);
    }

    public void ShowPrompt(bool shown)
    {
        promptObject.SetActive(shown);
    }

    public void SetOp1(string op1)
    {
        this.op1.text = "1: " + op1;
    }

    public void SetOp2(string op2)
    {
        this.op2.text = "2: " + op2;
    }

    public void SetOp3(string op3)
    {
        this.op3.text = "3: " + op3;
    }

    public void SetOp4(string op4)
    {
        this.op4.text = "4: " + op4;
    }

    public void PromptResponse(int response)
    {
        promptResponse = response;
    }

    private void AnimateUI()
    {
        uiAnimator.SetBool("TaskOpen", inputs.taskMenu);
        uiAnimator.SetBool("ShowHint", showHint);
    } 

    public void NewTask(string title, string info)
    {
        taskTitle.text = title;
        taskInfo.text = info;
        uiAnimator.SetTrigger("NewTask");
        audioManager.ding1.Play();
    }

    public void NewTaskTitle(string title)
    {
        taskTitle.text = title;
    }

    public void NewTaskInfo(string info)
    {
        taskInfo.text = info;
    }

    public void TaskAlert()
    {
        uiAnimator.SetTrigger("NewTask");
        audioManager.ding1.Play();
    }

    public void SetHint(string hint)
    {
        this.hint.text = hint;
    }
}