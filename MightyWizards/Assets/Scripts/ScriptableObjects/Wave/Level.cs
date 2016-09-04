using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Levels/Level")]
public class Level : ScriptableObject {

    [Tooltip("The rounds in this level")]
    public Round[] rounds;

    [Tooltip("How much time to wait after the last round finished spawning to start spawning the next")]
    public float delayBetweenRounds;
    [Tooltip("Every how many rounds should there be a break phase")]
    public float roundsBetweenBreaks;

    public void Initialize ()
    {
        foreach (Round round in rounds)
            round.Initialize();
    }

    public int GetRoundCount ()
    {
        return rounds.Length;
    }

    public void UpdateRound(int roundIndex)
    {
        rounds[roundIndex].Update();
    }

    public bool RoundFinishedSpawning(int roundIndex)
    {
        return rounds[roundIndex].FinishedSpawning();
    }

    public bool KilledAllInRound(int roundIndex)
    {
        return rounds[roundIndex].KilledAll();
    }

    public bool KilledAll ()
    {
        for (int i = 0; i < rounds.Length; ++i)
            if (!KilledAllInRound(i))
                return false;

        return true;
    }
}
