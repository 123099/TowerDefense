using UnityEngine;
using System.Collections;

public abstract class Spell : ScriptableObject {

    [Tooltip("The prefab to spawn when the spell is activated")]
    public GameObject spellPrefab;
    [Tooltip("The amount of times this spell can be used")]
    public int uses;

    protected int useCount;

    public void Activate ()
    {
        if(!HasUsesLeft()) return;

        Effect();

        ++useCount;
    }

    protected abstract void Effect ();

    public bool HasUsesLeft ()
    {
        return useCount < uses;
    } 
}
