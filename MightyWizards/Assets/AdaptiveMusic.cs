using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class AdaptiveMusic : MonoBehaviour {

    [Tooltip("Clips in increasing intensity")]
    [SerializeField] private AudioClip[] clips;

    private AudioSource[] audioSources;

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
    }

	void Update () {
        int index = GetClipIndex(GameUtils.GetAllEnemies().Length);
        if (index >= clips.Length) index = clips.Length - 1;
        if(index < 0) index = 0;
        for (int i = 0; i < clips.Length; ++i)
            if(i != index)
                audioSources[i].volume = 0;

        audioSources[index].volume += 0.01f;
    }

    private int GetClipIndex(int enemyCount)
    {
        if(enemyCount == 0) return 0;
        float clipIndexRaw = 0.9258f * Mathf.Log(enemyCount) - 0.5978f;
        return (int)Mathf.Round(clipIndexRaw);
    }
}
