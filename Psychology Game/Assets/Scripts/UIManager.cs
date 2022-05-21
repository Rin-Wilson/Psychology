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

    public GameObject upgradeMenu;
    [Space(10)]
    public Progress progressBar;
    public UnityEvent onWin;
    public UnityEvent onFail;
    void Start()
    {
        
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
    }

    //set iq display
    public void SetIq(int iq)
    {
        iqScore.text = "IQ points: " + iq;
    }

    //Set the lobe displays
    public void SetOccipital(int score)
    {
        occipitalScore.text = "Occipital: (" + score + ")";
    }

    public void SetFrontal(int score)
    {
        frontalScore.text = "Frontal: (" + score + ")";
    }

    public void SetTemporal(int score)
    {
        temporalScore.text = "Temporal: (" + score + ")";
    }

    public void SetParietal(int score)
    {
        parietalScore.text = "Parietal: (" + score + ")";
    }

    public void ShowUpgradeMenu(bool show)
    {
        upgradeMenu.SetActive(show);
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
            visualProgress_W = Mathf.Lerp(visualProgress_W, winProgress, 0.5f);
        }
        else
        {
            visualProgress_W = winProgress;
        }

        if (visualProgress_F < failProgress - 0.01 || visualProgress_F > failProgress + 0.01)
        {
            visualProgress_F = Mathf.Lerp(visualProgress_F, failProgress, 0.5f);
        }
        else
        {
            visualProgress_F = failProgress;
        }

        winBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualProgress_W);
        failBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, visualProgress_F);
    }
}
