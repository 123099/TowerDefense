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

    private void OnEnable ()
    {
        TurretSlot[] slots = GameUtils.GetBase().GetTurretSpawnPositions();
        for(int i = 0; i < slots.Length; ++i)
        {
            TurretSlot slot = slots[i];

            if (slot.IsFree())
            {
                Button slotButton = Instantiate(slotTemplate, slotTemplate.transform.parent) as Button;

                RectTransform slotButtonTransform = slotButton.GetComponent<RectTransform>();
                Vector3 positionOnScreen = Camera.main.WorldToScreenPoint(slot.transform.position);
                slotButtonTransform.localPosition = positionOnScreen;
            }
        }

        slotTemplate.gameObject.SetActive(false);
    }
}
