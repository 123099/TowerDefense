using UnityEngine;
using System.Collections;

public class GamePauser : MonoBehaviour {

    public void Pause ()
    {
        Time.timeScale = 0;
    }

    public void Unpause ()
    {
        Time.timeScale = 1;
    }

    public void TemporaryPause(float duration)
    {
        StartCoroutine(temporaryPause(duration));
    }

    private IEnumerator temporaryPause(float duration)
    {
        Pause();
        yield return new WaitForSecondsRealtime(duration);
        Unpause();
    }

    public bool IsPaused ()
    {
        return Time.timeScale == 0;
    }
}
