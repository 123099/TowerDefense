using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Levels/Level")]
public class Level : ScriptableObject {

    [Tooltip("The rounds in this level")]
    public Round[] rounds;

    public float delayBetweenRounds;
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

    public bool IsRoundComplete(int roundIndex)
    {
        return rounds[roundIndex].IsComplete();
    }
}
