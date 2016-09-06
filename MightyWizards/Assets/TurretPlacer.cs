using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TurretPlacer : MonoBehaviour {

    [SerializeField] private UnityEvent OnClose;

    [Tooltip("Turret slot button template")]
    [SerializeField] private Button slotTemplate;

    private Shop shop;
    private GameObject turretToPlace;

    private List<GameObject> spawnedButtons;

    public void Initialize(Shop shop, GameObject turret)
    {
        this.shop = shop;
        this.turretToPlace = turret;
    }

    private void OnEnable ()
    {
        spawnedButtons = new List<GameObject>();
        slotTemplate.gameObject.SetActive(false);

        TurretSlot[] slots = GameUtils.GetBase().GetTurretSpawnPositions();
        for(int i = 0; i < slots.Length; ++i)
        {
            TurretSlot slot = slots[i];

            Button slotButton = Instantiate(slotTemplate, slotTemplate.transform.parent) as Button;

            slotButton.gameObject.SetActive(true);

            RectTransform slotButtonTransform = slotButton.GetComponent<RectTransform>();
            Vector3 positionOnScreen = Camera.main.WorldToScreenPoint(slot.transform.position);
            slotButtonTransform.anchoredPosition = positionOnScreen;

            slotButtonTransform.localScale = Vector3.one;

            if (!slot.IsFree())
                slotButton.GetComponent<Image>().color = Color.red;
            else
                slotButton.GetComponent<Image>().color = Color.yellow;

            slotButton.onClick.AddListener(() =>
            {
                PlaceTurret(slot);
                OnClose.Invoke();
            });

            spawnedButtons.Add(slotButton.gameObject);
        }

        if(spawnedButtons.Count > 0)
            StartCoroutine(select(spawnedButtons[0]));
    }

    private void OnDisable ()
    {
        foreach (GameObject obj in spawnedButtons)
            Destroy(obj);
    }

    private void PlaceTurret(TurretSlot slot)
    {
        if (!slot.IsFree())
            RefundTurret(slot);

        turretToPlace.transform.SetParent(slot.transform);
        turretToPlace.transform.localPosition = Vector3.zero;
        turretToPlace.transform.localRotation = Quaternion.identity;
    }

    private void RefundTurret(TurretSlot slot)
    {
        GameObject turretInSlot = slot.GetTurret();
        for (int i = 0; i < shop.GetItemCount(); ++i)
            if (shop.GetItemAt(i).name.Equals(turretInSlot.name))
            {
                GameUtils.GetPlayer().GetComponent<ResourceInventory>().Add(shop.GetCurrency(), shop.GetItemAt(i).GetPrice() / 2);
                slot.Clear();
            }
    }

    private IEnumerator select (GameObject button)
    {
        yield return 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
