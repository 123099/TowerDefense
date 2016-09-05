using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour {

    [Tooltip("Called when the object is being destroyed")]
    [SerializeField] private UnityEvent OnDestroySelf;

	public void DestroySelf(float delay)
    {
        Destroy(gameObject, delay);
    }

    private void OnDestroy ()
    {
        OnDestroySelf.Invoke();
    }
}
