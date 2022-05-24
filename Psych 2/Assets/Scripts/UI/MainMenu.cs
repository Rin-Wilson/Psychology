using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int levelOneScene;
    [Header("Sub-Menus")]
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject quitMenu;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public void OnPlay()
    {
        StartCoroutine(LoadAsynchronously(levelOneScene));
    }

    public void OnCredits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnCreditsExit()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuit()
    {
        quitMenu.SetActive(true);
    }

    public void OnQuitCancel()
    {
        quitMenu.SetActive(false);
    }

    public void OnQuitConfirm()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            loadingBar.value = progress;

            yield return null;
        }
    }
}
