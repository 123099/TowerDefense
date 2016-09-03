using UnityEngine;
using System.Collections;

public class FreezePosition : MonoBehaviour {

	[SerializeField] private bool X;
    [SerializeField] private bool Y;
    [SerializeField] private bool Z;

    private Vector3 startPos;

    private void Start ()
    {
        startPos = transform.localPosition;
    }

    private void Update ()
    {
        Vector3 currentPos = transform.localPosition;

        if (X)
            currentPos.x = startPos.x;
        if (Y)
            currentPos.y = startPos.y;
        if (Z)
            currentPos.z = startPos.z;

        transform.localPosition = currentPos;
    }
}
