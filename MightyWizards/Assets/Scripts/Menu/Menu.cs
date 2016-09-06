using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[DisallowMultipleComponent]
public class Menu : MonoBehaviour {

    [Tooltip("The first button that appears in this menu")]
    [SerializeField] private Button firstButton;

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

    private void OnEnable ()
    {
        StartCoroutine(select());
    }

    private IEnumerator select ()
    {
        yield return 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
