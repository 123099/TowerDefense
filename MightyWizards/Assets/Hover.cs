using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	[Tooltip("The fraction of the distance the object makes every frame")]
    [SerializeField] private float smoothing;
    [Tooltip("The maximum distance the object moves away from it's start position")]
    [SerializeField] private float maxDistance;

    private Vector3 startPosition;
    private int direction = 1;

    private void OnEnable ()
    {
        startPosition = transform.position;
    }

    private void Update ()
    {
        if(GameUtils.IsGamePaused()) return;

        Vector3 destination = startPosition + transform.up * direction * maxDistance;
        transform.position = Vector3.Slerp(transform.position, destination, smoothing);
        
        if (Vector3.Distance(transform.position, destination) < 0.1f)
            direction *= -1;
    }

    public void SetSmoothing(float smoothing)
    {
        this.smoothing = smoothing;
    }

    public void SetMaxDistance(float maxDistance)
    {
        this.maxDistance = maxDistance;
    }
}
