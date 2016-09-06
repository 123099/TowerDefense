using UnityEngine;
using System.Collections;

public class TurretSlot : MonoBehaviour {

	public bool IsFree ()
    {
        return transform.childCount == 0;
    }

    public GameObject GetTurret ()
    {
        return transform.GetChild(0).gameObject;
    }

    public void Clear ()
    {
        if (!IsFree())
            Destroy(GetTurret().gameObject);
    }
}
