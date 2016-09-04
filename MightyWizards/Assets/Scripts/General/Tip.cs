using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tip : MonoBehaviour {

    [Tooltip("The text area where tips will be shown")]
    [SerializeField] private Text tipUI;
    [Tooltip("A list of tips to display. These are chosen at random.")]
    [SerializeField] private string[] tips;

	private void OnEnable ()
    {
        if(tips.Length == 0) return;
        tipUI.text = tips[Random.Range(0, tips.Length)];
    }
}
