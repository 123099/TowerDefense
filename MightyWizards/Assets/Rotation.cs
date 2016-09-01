using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	private void Update ()
    {
        transform.Rotate(0, 5 * Time.deltaTime, 0);
    }
}
