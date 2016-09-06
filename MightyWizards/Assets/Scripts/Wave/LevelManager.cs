using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    [Tooltip("The level this manager is responsible of")]
    [SerializeField] private Level level;

    [SerializeField] private UnityEvent OnLevelStart;
    [SerializeField] private RoundEvent OnRoundComplete;
    [SerializeField] private UnityEvent OnLevelComplete;
    [SerializeField] private UnityEvent OnBreakPhaseStart;

    private int currentRound;
    private int lastBreakRound;

    private RateTimer roundTimer;

	void Start () {
        level.Initialize();
        currentRound = 0;
        lastBreakRound = 0;
        roundTimer = new RateTimer(1f/level.delayBetweenRounds);

        OnLevelStart.Invoke();
	}

    private void Update ()
    {
        level.UpdateRound(currentRound);

        if (level.KilledAll())
        {
            enabled = false;
            OnLevelComplete.Invoke();
        }
        else
        {
            if(level.KilledAllInRound(currentRound))
            {
                roundTimer.SetLastReadyTimeOnce(Time.time);
                if (!IsBreakTime() && roundTimer.IsReady())
                {
                    print("Round complete " + currentRound);
                    ++currentRound;
                    print("Next round " + currentRound);
                    OnRoundComplete.Invoke(currentRound);
                }
            }

            if (IsBreakTime())
            {
                if (level.KilledAllInRound(currentRound))
                {
                    print("Break phase in round " + currentRound);
                    OnBreakPhaseStart.Invoke();
                    lastBreakRound = currentRound;
                }
            }
        }
    }

    private bool IsBreakTime ()
    {
        //return currentRound + 1 - lastBreakRound >= level.roundsBetweenBreaks;
        return (currentRound + 1) % level.roundsBetweenBreaks == 0;
    }

    private bool IsInLastRound ()
    {
        return currentRound >= level.GetRoundCount() - 1;
    }
}
