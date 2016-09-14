using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    [Tooltip("The shake multiplier")]
    [SerializeField] private float multiplier;

	public void Shake(float duration)
    {
        StartCoroutine(shake(duration));
    }

    private IEnumerator shake (float duration)
    {

        float elapsed = 0.0f;

        Vector3 originalPos = transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Mathf.PerlinNoise(Random.value, Random.value) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(Random.value, Random.value) * 2.0f - 1.0f;
            x *= multiplier * damper;
            y *= multiplier * damper;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            yield return null;
        }

        transform.position = originalPos;
    }
}
