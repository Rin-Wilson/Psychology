using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    public InputManager inputs;
    public HUD ui;
    public GameObject popup;
    public GameObject mainCam;
    public AudioSource clickSFX;
    public ResistTheId idGame;
    public TMPro.TextMeshProUGUI searchbar;
    public bool open;
    [Header("UI")]
    public GameObject screen;
    public GameObject browser;
    public bool isSearching;
    public bool hasPlayed;
    public string targetWord;
    public float idCountDown;
    public float idCountDownDelta;
    public int wordIndex;

    public GameObject resultsPage;

    public GameObject[] objectsToHide;

    public override void triggerInteract()
    {
        open = true;
        OpenComputer();
    }

    public void ShutDown()
    {
        open = false;
        OpenComputer();
    }

    private void Start()
    {
        mainCam = GameObject.Find("/Main Camera");
    }

    private void Update()
    {
        
        popup.SetActive(isSelected);
        popup.transform.LookAt(mainCam.transform);

        if (isSearching)
        {
            search();
            if(idCountDownDelta <= 0 && !idGame.active)
            {
                searchbar.text = "";
                idGame.gameObject.SetActive(true);
                idGame.active = true;
            }
            else
            {
                idCountDownDelta -= Time.deltaTime;
            }

        }

        if (ui.storedMisinformation != null)
        {
            targetWord = ui.storedMisinformation;
        }

        if(searchbar.text == targetWord)
        {

        }
    }

    public void OnOpenBrowser()
    {
            clickSFX.Play();
            browser.SetActive(true);
    }

    public void OnCloseBrowser()
    {
        clickSFX.Play();
        browser.SetActive(false);
        isSearching = false;
    }

    private void OpenComputer()
    {
        if (open)
        {
            screen.SetActive(true);
            foreach(GameObject g in objectsToHide)
            {
                g.SetActive(false);
            }
        }
        else
        {
            screen.SetActive(false);
            foreach (GameObject g in objectsToHide)
            {
                g.SetActive(true);
            }
        }
    }

    public void Searching()
    {
        isSearching = true;
        idCountDownDelta = idCountDown;
        searchbar.text = "";
    }

    private void search()
    {
        if (inputs.type && !idGame.active)
        {
            if (wordIndex < targetWord.Length)
            {
                searchbar.text += targetWord.Substring(wordIndex, 1);
                wordIndex++;
                inputs.type = false;
            }
        }
    }
}
