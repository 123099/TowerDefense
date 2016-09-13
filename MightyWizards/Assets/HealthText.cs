using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class HealthText : MonoBehaviour {

	public void UpdateHealth (float health)
    {
        GetComponent<Text>().text = (health * 100).ToString("0") + "%";
    }
}
