using UnityEngine;
using System.Collections;

public class DisableOnAwake : MonoBehaviour {

	private void Awake ()
    {
        gameObject.SetActive(false);
    }
}
