using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour {

    [SerializeField]
    private Canvas loadingCanvas;
    [SerializeField]
    private RectTransform loadingPanel;
    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private UIFader uiFader;
    [SerializeField]
    private bool fadeOnLoad = true;

    private bool switching = false;
	
    private void Awake()
    {
        loadingCanvas.gameObject.SetActive(false);
        loadingPanel.gameObject.SetActive(false);
        uiFader.gameObject.SetActive(true);

        if (fadeOnLoad)
            SceneManager.sceneLoaded += onLevelLoad;
    }

    public void NextScene(float delay = 0)
    {
        int nextID = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextID >= SceneManager.sceneCountInBuildSettings)
        {
            print("Reached last scene.");
            return;
        }

        SwitchScene(nextID, delay);
    }

    public void PreviousScene(float delay = 0)
    {
        int previousID = SceneManager.GetActiveScene().buildIndex - 1;
        if (previousID < 0)
        {
            print("Reached first scene.");
            return;
        }

        SwitchScene(previousID, delay);
    }

    public void ReloadCurrentScene(float delay = 0)
    {
        SwitchScene(SceneManager.GetActiveScene().buildIndex, delay);
    }

    public void SwitchScene(int index)
    {
        SwitchScene(index, 0);
    }

	public void SwitchScene(int index, float delay)
    {
        if (switching) return;
        switching = true;
        StartCoroutine(fadeOutToScene(index, delay));
    }

    public void SwitchScene(string sceneName, float delay = 0)
    {
        if (switching) return;
        switching = true;
        StartCoroutine(fadeOutToScene(sceneName, delay));
    }

    public void QuitGame(float delay = 0)
    {
#if UNITY_EDITOR
        Application.Quit();
#else
        StartCoroutine(fadeOutAndQuit(delay));
#endif
    }

    private IEnumerator fadeOutAndQuit(float delay)
    {
        yield return new WaitForSecondsUnscaled(delay);
        loadingCanvas.gameObject.SetActive(true);
        float fadeTime = uiFader.Fade(UIFader.FadeMode.FadeOut);
        yield return new WaitForSecondsUnscaled(fadeTime);
        Application.Quit();
    }   

    private IEnumerator fadeOutToScene(string sceneName, float delay)
    {
        yield return new WaitForSecondsUnscaled(delay);

        yield return fadeOut();

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        yield return loadScene(async);
    }

    private IEnumerator fadeOutToScene(int index, float delay)
    {
        yield return new WaitForSecondsUnscaled(delay);

        yield return fadeOut();

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        yield return loadScene(async);
    }

    private IEnumerator fadeOut()
    {
        loadingCanvas.gameObject.SetActive(true);

        float fadeTime = uiFader.Fade(UIFader.FadeMode.FadeOut);
        yield return new WaitForSecondsUnscaled(fadeTime);

        loadingPanel.gameObject.SetActive(true);

        fadeTime = uiFader.Fade(UIFader.FadeMode.FadeIn);
        yield return new WaitForSecondsUnscaled(fadeTime);

        System.GC.Collect();
    }

    private IEnumerator loadScene(AsyncOperation async)
    {
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return 0;
        }
    }

    private void onLevelLoad(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(fadeIn());
    }

    private IEnumerator fadeIn()
    {
        loadingCanvas.gameObject.SetActive(true);

        float fadeTime = uiFader.Fade(UIFader.FadeMode.FadeIn);
        yield return new WaitForSecondsUnscaled(fadeTime);

        loadingCanvas.gameObject.SetActive(false);
    }

    private void OnDestroy ()
    {
        SceneManager.sceneLoaded -= onLevelLoad;
    }
}
