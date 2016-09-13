using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class RoundAnnouncer : MonoBehaviour {

    public void DisplayRound(int roundNumber)
    {
        Text text = GetComponent<Text>();
        Color color = text.color;
        color.a = 0;
        text.color = color;

        text.text = "Round " + roundNumber;
        StartCoroutine(displayMessage());
    }

    private IEnumerator displayMessage ()
    {
        Text text = GetComponent<Text>();
        Color color = text.color;

        yield return new WaitForSeconds(1f);

        for (float t = 0; t <= 1; t += 0.03f)
        {
            color.a = t;
            text.color = color;
            yield return 0;
        }

        yield return new WaitForSeconds(2f);

        for (float t = 1; t >= 0; t -= 0.03f)
        {
            color.a = t;
            text.color = color;
            yield return 0;
        }

        color.a = 0;
        text.color = color;
    }
}
