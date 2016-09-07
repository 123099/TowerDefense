using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class TargetFollower : MonoBehaviour {

    [Tooltip("The transform the camera will follow")]
    [SerializeField] private Transform target;
    [Tooltip("The distance away from the borders of the camera at which the camera will start following the target")]
    [SerializeField] [Range(0,0.5f)] private float startDistance;
    [Tooltip("The speed with which the camera moves")]
    [SerializeField] private float moveSpeed;
    [Tooltip("The maximum distance from the starting position the camera will move")]
    [SerializeField] private float maxMoveDistance;

    private Vector3 startPos;

    private Camera camera;

    private void Start ()
    {
        camera = GetComponent<Camera>();
        startPos = transform.position;
    }

    private void Update ()
    {
        if (targetWithinMoveArea())
            Move();
    }

	private bool targetWithinMoveArea ()
    {
        Vector3 viewportPos = camera.WorldToViewportPoint(target.position);
        return Mathf.Abs(viewportPos.x - 0.5f) >= 0.5f - startDistance;
    }

    private void Move ()
    {
        Vector3 dir = target.position - transform.position;
        float dirSign = Mathf.Sign(dir.x);

        Vector3 startToTransform = transform.position - startPos;
        float startToTransformSign = Mathf.Sign(startToTransform.x);

        if (dirSign == startToTransformSign && startToTransform.magnitude > maxMoveDistance)
            return;

        transform.position = Vector3.Slerp(transform.position, transform.position + Vector3.right * dirSign * moveSpeed * Time.deltaTime, 0.6f);
    }

    public void ResetPosition ()
    {
        transform.position = startPos;
    }
}
