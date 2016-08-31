using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tip : MonoBehaviour {

    [SerializeField]
    private Text tipUI;
    [SerializeField]
    private string[] tips;

	private void OnEnable ()
    {
        if(tips.Length == 0) return;
        tipUI.text = tips[Random.Range(0, tips.Length)];
    }
}
