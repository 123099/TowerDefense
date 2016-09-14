using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    [Tooltip("The speed with which the tranform will rotate around the y axis")]
    [SerializeField] private float speed;

	private void Update ()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
