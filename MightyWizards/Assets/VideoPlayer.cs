using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

	private void Awake ()
    {
        ( (MovieTexture)GetComponent<Renderer>().material.mainTexture ).Play();
    }
}
