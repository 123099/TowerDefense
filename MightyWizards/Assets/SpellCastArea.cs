using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpellCastArea : MonoBehaviour {

    [Tooltip("Set to true to only call enter area if spell has uses left")]
    [SerializeField] private bool considerSpellUses;

    [SerializeField] private UnityEvent OnEnterArea;
    [SerializeField] private UnityEvent OnLeaveArea;

	private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Player>())
            if(!considerSpellUses || (considerSpellUses && col.gameObject.GetComponent<Player>().GetStaff().spell.HasUsesLeft()))
                OnEnterArea.Invoke();
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Player>())
            if (!considerSpellUses || ( considerSpellUses && col.gameObject.GetComponent<Player>().GetStaff().spell.HasUsesLeft() ))
                OnLeaveArea.Invoke();
    }
}
