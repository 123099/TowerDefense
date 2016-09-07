using UnityEngine;
using System.Collections;

public class TurretSlot : MonoBehaviour {

	public bool IsFree ()
    {
        return transform.childCount == 0;
    }

    public Turret GetTurret ()
    {
        return transform.GetChild(0).GetComponent<Turret>();
    }

    public void Clear ()
    {
        if (!IsFree())
            Destroy(GetTurret().gameObject);
    }
}
