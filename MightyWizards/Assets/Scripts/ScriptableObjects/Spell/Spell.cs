using UnityEngine;
using System.Collections;

public abstract class Spell : ScriptableObject {

    [Tooltip("The prefab to spawn when the spell is activated")]
    [SerializeField] protected GameObject spellPrefab;
    [Tooltip("The amount of times this spell can be used")]
    [SerializeField] private int uses;
    [Tooltip("Whether the spell is passive or active")]
    [SerializeField] private bool passive;

    protected int useCount;

    public void Initialize ()
    {
        useCount = 0;
    }

    public void Activate ()
    {
        if(!HasUsesLeft() || passive) return;

        Effect();

        ++useCount;
    }

    public void UpdatePassive ()
    {
        if(!passive) return;
        Effect();
    }

    protected abstract void Effect ();

    public virtual void PassiveStart () { }
    public virtual void PassiveStop () { }

    public bool HasUsesLeft ()
    {
        return useCount < uses;
    } 
}
