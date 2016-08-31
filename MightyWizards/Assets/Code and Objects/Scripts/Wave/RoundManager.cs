using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour {

    [SerializeField]
    [Tooltip("The rounds in this level")]
    private Round[] rounds;

    [SerializeField] private float delayBetweenRounds;
    [SerializeField] private float roundsBetweenBreaks;

    [SerializeField] private RoundEvent OnRoundComplete;
    [SerializeField] private UnityEvent OnLevelComplete;
    [SerializeField] private UnityEvent OnBreakPhaseStart;

    private int currentRound;
    private int lastBreakRound;

    private bool paused;
    private bool complete;

	void Start () {
        if (rounds.Length == 0)
            complete = true;

        currentRound = 0;
        lastBreakRound = 0;

        foreach (Round round in rounds)
            round.Initialize();

        StartCoroutine(updateLoop());
	}

    private IEnumerator updateLoop ()
    {
        while (true)
        {
            if (paused)
            {
                yield return 0;
                continue;
            }

            if (currentRound >= rounds.Length)
            {
                complete = true;
                OnLevelComplete.Invoke();
            }

            if (complete)
                break;

            rounds[currentRound].Update();

            if (rounds[currentRound].IsComplete())
            {
                OnRoundComplete.Invoke(currentRound);
                yield return new WaitForSeconds(delayBetweenRounds);
                ++currentRound;

                if(currentRound - lastBreakRound >= roundsBetweenBreaks)
                {
                    Pause();
                    OnBreakPhaseStart.Invoke();
                }
            }

            yield return 0;
        }
    }

    public bool IsLevelComplete ()
    {
        return complete;
    }

    public void Pause ()
    {
        paused = true;
    }

    public void Resume ()
    {
        paused = false;
    }
}
