using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    [Tooltip("The current menu that is open")]
    [SerializeField] private Menu currentMenu;
    private Menu previousMenu;

    [Tooltip("A sound that will play every time a new menu is open. If there is no audio component on this object, the sound won't be played")]
    [SerializeField] private AudioClip transitionSound;
    private AudioSource audioSource;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable ()
    {
        ShowMenu(currentMenu);
    }

	public void ShowMenu(Menu menu)
    {
        previousMenu = currentMenu;

        if (currentMenu != null)
            currentMenu.IsOpen = false;

        currentMenu = menu;
        currentMenu.IsOpen = true;

        if (audioSource)
            audioSource.PlayOneShot(transitionSound);
    }

    public void ShowPreviousMenu ()
    {
        ShowMenu(previousMenu);
    }
}
