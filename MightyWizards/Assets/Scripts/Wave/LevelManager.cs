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

    private RateTimer roundTimer;

	void Start () {
        level.Initialize();
        currentRound = 0;
        lastBreakRound = 0;
        roundTimer = new RateTimer(1f/level.delayBetweenRounds);
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
            if(level.RoundFinishedSpawning(currentRound))
            {
                roundTimer.SetLastReadyTimeOnce(Time.time);
                if (!IsBreakTime() && roundTimer.IsReady())
                {
                    ++currentRound;
                    OnRoundComplete.Invoke(currentRound);
                }
            }

            if (IsBreakTime())
            {
                if (level.KilledAllInRound(currentRound))
                {
                    OnBreakPhaseStart.Invoke();
                    lastBreakRound = currentRound;
                }
            }
        }
    }

    private bool IsBreakTime ()
    {
        return currentRound + 1 - lastBreakRound >= level.roundsBetweenBreaks;
    }

    private bool IsInLastRound ()
    {
        return currentRound >= level.GetRoundCount() - 1;
    }
}
