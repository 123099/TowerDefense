using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class TargetFollower : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] [Range(0,0.5f)] private float startDistance;
    [SerializeField] private float moveSpeed;
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

        transform.Translate(Vector3.left * dirSign * moveSpeed * Time.deltaTime);
    }

    public void ResetPosition ()
    {
        transform.position = startPos;
    }
}
