using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Pan : MonoBehaviour {

    [Tooltip("The factor by which to blend between the start and target positions each frame")]
    [SerializeField] private float smoothing;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool pan;

	public void PanFrom(Transform startPosition)
    {
        this.startPosition = startPosition.position;
        targetPosition = transform.position;

        pan = true;
    }

    private void Update ()
    {
        if (pan)
        {
            startPosition = Vector3.Slerp(startPosition, targetPosition, smoothing);
            transform.position = startPosition;
        }
    }
}
