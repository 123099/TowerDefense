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

    private RateTimer roundTimer;

	void Start () {
        level.Initialize();
        currentRound = 0;
        roundTimer = new RateTimer(1f/level.delayBetweenRounds);

        OnLevelStart.Invoke();
	}

    private void Update ()
    {
        level.UpdateRound(currentRound);

        if (level.KilledAll())
        {
            OnLevelComplete.Invoke();
            enabled = false;
            return;
        }
        else
        {
            if (level.KilledAllInRound(currentRound))
            {
                roundTimer.SetLastReadyTimeOnce(Time.time);
                if (roundTimer.IsReady())
                {
                    ++currentRound;
                    OnRoundComplete.Invoke(currentRound);
                    if (IsBreakTime())
                    {
                        OnBreakPhaseStart.Invoke();
                    }
                }
            }
        }
    }

    private bool IsBreakTime ()
    {
        return (currentRound + 1) % level.roundsBetweenBreaks == 0;
    }
}
