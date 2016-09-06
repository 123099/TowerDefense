using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class AdaptiveMusic : MonoBehaviour {

    [Tooltip("Clips in increasing intensity")]
    [SerializeField] private AudioClip[] clips;

    private AudioSource[] audioSources;

    private bool firstPlay;
    private bool paused;

    private void Awake ()
    {
        audioSources = new AudioSource[clips.Length];
        for (int i = 0; i < clips.Length; ++i)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = clips[i];
            audioSources[i].loop = true;
            audioSources[i].volume = 0;
            audioSources[i].Play();
        }

        firstPlay = true;
    }

	void Update () {
        if(paused) return;

        int index = GetCurrentClipIndex(GameUtils.GetAllEnemies().Length);
        
        for (int i = 0; i < clips.Length; ++i)
            if(i != index)
                audioSources[i].volume = 0;

        if (firstPlay)
        {
            audioSources[index].volume += 0.01f;
            if (audioSources[index].volume > 0.98f)
                firstPlay = false;
        }
        else
            audioSources[index].volume = 1f;
    }

    private int GetCurrentClipIndex(int enemyCount)
    {
        if(enemyCount == 0) return 0;
        float clipIndexRaw = 0.9258f * Mathf.Log(enemyCount) - 0.5978f;
        int index = (int)Mathf.Round(clipIndexRaw);

        if (index >= clips.Length) index = clips.Length - 1;
        if(index < 0) index = 0;

        return index;
    }

    public void Stop ()
    {
        paused = true;
        audioSources[GetCurrentClipIndex(GameUtils.GetAllEnemies().Length)].Pause();
    }

    public void Play ()
    {
        paused = false;
        audioSources[GetCurrentClipIndex(GameUtils.GetAllEnemies().Length)].UnPause();
    }
}
