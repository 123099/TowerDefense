using UnityEngine;
using System.Collections;

public class TurretAnimationEvent : MonoBehaviour {

	public void Shoot ()
    {
        transform.root.GetComponent<Turret>().Shoot();
    }
}
