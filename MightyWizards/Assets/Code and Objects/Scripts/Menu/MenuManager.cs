using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Menu currentMenu;
    private Menu previousMenu;

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
        print("showing menu");
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
