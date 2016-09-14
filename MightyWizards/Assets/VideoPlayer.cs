using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class VideoPlayer : MonoBehaviour {

    public UnityEvent OnComplete;

    private MovieTexture texture;

	private void Awake ()
    {
        texture = ( (MovieTexture)GetComponent<Renderer>().material.mainTexture );
        texture.Play();
        GetComponent<AudioSource>().clip = texture.audioClip;
        GetComponent<AudioSource>().Play();
    }

    private void Update ()
    {
        if (!texture.isPlaying)
            OnComplete.Invoke();
        
    }
}
