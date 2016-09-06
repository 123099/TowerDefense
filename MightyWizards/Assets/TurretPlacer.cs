using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretPlacer : MonoBehaviour {

    [Tooltip("Turret slot button template")]
    [SerializeField] private Button slotTemplate;

    private Turret[] turretsToPlace;

    public void SetTurrets(Turret[] turrets)
    {
        turretsToPlace = turrets;
    }
}
