using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Pan : MonoBehaviour {

    [Tooltip("The factor by which to blend between the start and target positions each frame")]
    [SerializeField] private float smoothing;

    public Transform start;
    public bool startOnAwake;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool pan;

    private void Awake ()
    {
        if (startOnAwake)
            PanFrom(start);
    }

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

            if (Vector3.Distance(transform.position, targetPosition) < 1f)
                pan = false;
        }
    }
}
