using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
