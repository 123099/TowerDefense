using UnityEngine;
using System.Collections;

public class PlayerAnimationEvent : MonoBehaviour {

	public void Attack ()
    {
        transform.parent.GetComponent<Player>().Attack();
    }

    public void CastSpell ()
    {
        transform.parent.GetComponent<Player>().CastSpell();
    }
}
