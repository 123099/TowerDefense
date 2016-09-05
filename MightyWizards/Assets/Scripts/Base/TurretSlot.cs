using UnityEngine;
using System.Collections;

public class TurretSlot : MonoBehaviour {

	public bool IsFree ()
    {
        return transform.childCount == 0;
    }
}
