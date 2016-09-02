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

    public bool IsComplete ()
    {
        foreach(Wave wave in waves)
            if (!wave.IsComplete())
                return false;

        return true;
    }
}
