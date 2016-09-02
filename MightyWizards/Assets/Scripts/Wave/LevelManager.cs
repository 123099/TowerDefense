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
	}

    private void Update ()
    {
        level.UpdateRound(currentRound);

        if (level.KilledAllInRound(currentRound))
        {
            if (!IsInLastRound() && IsBreakTime())
            {
                OnBreakPhaseStart.Invoke();
                lastBreakRound = currentRound;
            }
        }
        else if (!IsBreakTime() && level.RoundFinishedSpawning(currentRound))
        {
            if (!IsInLastRound())
            {
                ++currentRound;
                OnRoundComplete.Invoke(currentRound);
            }
        }

        if (level.KilledAll())
        {
            OnLevelComplete.Invoke();
            enabled = false;
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
