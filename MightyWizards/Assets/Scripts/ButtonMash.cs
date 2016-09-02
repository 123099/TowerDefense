using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ButtonMash : MonoBehaviour {

    [SerializeField] private string buttonToMash;
    [SerializeField] private float progressGravity;
    [SerializeField] private float timeUntilFail;

    [SerializeField] private MashStringEvent OnMashStart;
    [SerializeField] private UnityEvent OnMashSuccessful;
    [SerializeField] private UnityEvent OnMashFail;
    [SerializeField] private MashFloatEvent OnMashProgress;

    private float progress;
    private float lastButtonClickTime;

    private float timeProgressZero;

    private bool paused;

    public void StartMash ()
    {
        paused = false;
        progress = 0;
        lastButtonClickTime = Time.time;
        timeProgressZero = 0;

        OnMashStart.Invoke("Mash " + buttonToMash);
    }

    public void PauseMash ()
    {
        paused = true;
    }

	private void Update ()
    {
        if(paused) return;

        progress /= progressGravity;

        if (Input.GetButtonDown(buttonToMash))
        {
            progress += 1f / ( Time.time - lastButtonClickTime ) / 10;
            lastButtonClickTime = Time.time;
        }

        OnMashProgress.Invoke(progress);

        if (progress >= 1)
        {
            OnMashSuccessful.Invoke();
            PauseMash();
        }

        if (progress <= 0)
            timeProgressZero += Time.deltaTime;

        if (timeProgressZero > timeUntilFail)
        {
            OnMashFail.Invoke();
            PauseMash();
        }
    }
}
