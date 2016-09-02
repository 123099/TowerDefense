using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private Level level;

    [SerializeField] private RoundEvent OnRoundComplete;
    [SerializeField] private UnityEvent OnLevelComplete;
    [SerializeField] private UnityEvent OnBreakPhaseStart;

    private int currentRound;
    private int lastBreakRound;

	void Start () {
        level.Initialize();
        currentRound = 0;
        lastBreakRound = 0;

        StartCoroutine(updateLoop());
	}

    private IEnumerator updateLoop ()
    {
        while (true)
        {
            if (IsComplete())
            {
                OnLevelComplete.Invoke();
                break;
            }

            level.UpdateRound(currentRound);

            if (level.IsRoundComplete(currentRound))
                yield return doRoundComplete();

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator doRoundComplete ()
    {
        OnRoundComplete.Invoke(currentRound);
        yield return new WaitForSeconds(level.delayBetweenRounds);
        ++currentRound;

        if (currentRound - lastBreakRound >= level.roundsBetweenBreaks)
        {
            OnBreakPhaseStart.Invoke();
            lastBreakRound = currentRound;
        }
    }

    public bool IsComplete ()
    {
        return currentRound >= level.GetRoundCount();
    }
}
