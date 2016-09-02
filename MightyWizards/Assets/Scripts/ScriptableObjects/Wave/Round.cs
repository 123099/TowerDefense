using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Levels/Round")]
public class Round : ScriptableObject {

    [Tooltip("The waves that will spawn during this round")]
    public Wave[] waves;

    public void Initialize ()
    {
        foreach (Wave wave in waves)
            wave.Initialize();
    }

    public void Update ()
    {
        foreach (Wave wave in waves)
            wave.Update();
    }

    public bool FinishedSpawning ()
    {
        foreach(Wave wave in waves)
            if (!wave.FinishedSpawning())
                return false;

        return true;
    }

    public bool KilledAll ()
    {
        foreach (Wave wave in waves)
            if (!wave.KilledAll())
                return false;

        return true;
    }
}
