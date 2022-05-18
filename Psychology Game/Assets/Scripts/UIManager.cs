using UnityEngine;

public class UIManager : MonoBehaviour
{
    //References to the text fields
    public TMPro.TextMeshProUGUI iqScore;

    public TMPro.TextMeshProUGUI occipitalScore;
    public TMPro.TextMeshProUGUI frontalScore;
    public TMPro.TextMeshProUGUI temporalScore;
    public TMPro.TextMeshProUGUI parietalScore;

    public GameObject upgradeMenu;
    void Start()
    {
        
    }

    void Update()
    {
        
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
