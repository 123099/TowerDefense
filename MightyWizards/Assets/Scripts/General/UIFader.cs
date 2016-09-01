using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIFader : MonoBehaviour {

    public enum FadeMode { FadeIn = -1, FadeOut = 1 }
    [SerializeField]
    private float fadeTime = 0.8f;

    private Image image;

    private FadeMode fadeMode;
    private float alpha;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private IEnumerator fade()
    {
        float start = fadeMode == FadeMode.FadeIn ? 1 : 0;
        float end = fadeMode == FadeMode.FadeIn ? 0 : 1;
        Color c = image.color;
        for (float t = 0; t <= 1f; t += Time.unscaledDeltaTime / fadeTime)
        {
            if (image == null) break;
            alpha = Mathf.Lerp(start, end, t);
            image.color = new Color(c.r, c.g, c.b, alpha);
            yield return 0;
        }
    }

    public float Fade(FadeMode fadeMode)
    {
        this.fadeMode = fadeMode;
        StartCoroutine(fade());
        return fadeTime;
    }
}
