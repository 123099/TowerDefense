using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpellCastArea : MonoBehaviour {

    [SerializeField] private UnityEvent OnEnterArea;
    [SerializeField] private UnityEvent OnLeaveArea;

	private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Player>())
            OnEnterArea.Invoke();
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Player>())
            OnLeaveArea.Invoke();
    }
}
