using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ButtonMash : MonoBehaviour {

    [Tooltip("The name of the button as defined in the Input settings that needs to be mashed")]
    [SerializeField] private string buttonToMash;
    [Tooltip("By how much should the progress be divided every frame. For example, 2 would mean the progress loses 50% of itself every frame")]
    [SerializeField] private float progressGravity;
    [Tooltip("How much time do you have to successfully mash the button after the mashing started")]
    [SerializeField] private float timeUntilFail;

    [SerializeField] private MashStringEvent OnMashStart;
    [SerializeField] private UnityEvent OnMashSuccessful;
    [SerializeField] private UnityEvent OnMashFail;
    [SerializeField] private MashFloatEvent OnMashProgress;

    private float progress;
    private float lastButtonClickTime;

    private float timeProgressZero;

    private bool paused = true;

    public void StartMash ()
    {
        paused = false;
        progress = 0;
        lastButtonClickTime = 0;
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
            progress += 1f / ( Time.time - lastButtonClickTime ) / 15;
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
