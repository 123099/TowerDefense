using UnityEngine;
using System.Collections;

public abstract class Spell : ScriptableObject {

    [Tooltip("The prefab to spawn when the spell is activated")]
    public GameObject spellPrefab;

    public abstract void Activate ();
}
