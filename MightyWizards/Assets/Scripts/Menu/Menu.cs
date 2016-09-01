using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Menu : MonoBehaviour {

	public bool IsOpen
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    private void Awake ()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.offsetMin = rect.offsetMax = Vector2.zero;

        gameObject.SetActive(false);
    }
}
