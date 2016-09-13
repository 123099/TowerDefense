using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class HeightDeath : MonoBehaviour {

    [Tooltip("The height at which the entity will instantly die")]
	[SerializeField] private float deathHeight = -5;

    [SerializeField] private UnityEvent OnDeath;

    private void Update ()
    {
        if (transform.position.y < deathHeight)
        {
            OnDeath.Invoke();
            enabled = false;
        }
    }
}
