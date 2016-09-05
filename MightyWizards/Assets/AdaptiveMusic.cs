using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class AdaptiveMusic : MonoBehaviour {

    [Tooltip("Clips in increasing intensity")]
    [SerializeField] private AudioClip[] clips;

    private AudioSource audio1;
    private AudioSource audio2;

    private Coroutine fadingCoroutine;

    private void Awake ()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        audio1 = audios[0];
        audio2 = audios[1];
    }

	void Update () {
        int index = GetClipIndex(GameUtils.GetAllEnemies().Length);
        if (index >= clips.Length) index = clips.Length - 1;

        if (audio1.clip != clips[index] && audio2.clip != clips[index])
        {
            StartCoroutine(FadeToClip(clips[index]));
        }
    }

    private IEnumerator FadeToClip(AudioClip clip)
    {
        AudioSource nextSource = audio1.clip ? audio2 : audio1;
        AudioSource oldSource = audio1.clip ? audio1 : audio2;
        AudioClip oldClip = audio1.clip ? audio1.clip : audio2.clip;

        nextSource.clip = clip;
        nextSource.volume = 0;
        nextSource.Play();

        oldSource.volume = 1;

        for(float i = 0; i <= 1f; i += 0.01f)
        {
            nextSource.volume = i;
            oldSource.volume = 1 - i;
            yield return 0;
        }

        oldSource.clip = null;
    }

    private int GetClipIndex(int enemyCount)
    {
        if(enemyCount == 0) return 0;
        float clipIndexRaw = 0.8604f * Mathf.Log(enemyCount) + 0.0461f;
        print("enemy count - " + enemyCount + ": " + clipIndexRaw);
        return (int)Mathf.Round(clipIndexRaw);
    }
}
