using UnityEngine;
using System.Collections;

public class PlayerIgnoreCollisionExit : MonoBehaviour {

    [Tooltip("The collider of the platform the player is ignoring collisions with")]
    [SerializeField] private Collider platformCollider;

	private void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Player>())
            Physics.IgnoreCollision(col, platformCollider, false);
    }
}
