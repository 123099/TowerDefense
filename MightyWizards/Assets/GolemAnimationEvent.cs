using UnityEngine;
using System.Collections;

public class GolemAnimationEvent : MonoBehaviour {

	public void Attack ()
    {
        transform.parent.GetComponent<Enemy>().Attack();
    }
}
