using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TurretPlacer : MonoBehaviour {

    [Tooltip("Turret slot button template")]
    [SerializeField] private Button slotTemplate;
    [Tooltip("The color for a taken slot")]
    [SerializeField] private Color takenSlotColor;
    [Tooltip("The color for a free slot")]
    [SerializeField] private Color freeSlotColor;

    [SerializeField] private UnityEvent OnClose;

    private Shop shop;
    private Turret turretToPlace;

    private List<GameObject> spawnedButtons;

    public void Initialize(Shop shop, Turret turret)
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
                slotButton.GetComponent<Image>().color = takenSlotColor;
            else
                slotButton.GetComponent<Image>().color = freeSlotColor;

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

    private void Update ()
    {
        if(Input.GetButtonDown("Cancel") && turretToPlace)
        {
            RefundTurret(turretToPlace, 1f);
            OnClose.Invoke();
        }
    }

    private void PlaceTurret(TurretSlot slot)
    {
        if (!slot.IsFree())
        {
            RefundTurret(slot.GetTurret(), 0.5f);
            slot.Clear();
        }

        turretToPlace.transform.SetParent(slot.transform);
        turretToPlace.transform.localPosition = Vector3.zero;
        turretToPlace.transform.localRotation = Quaternion.identity;

        turretToPlace = null;
    }

    private void RefundTurret(Turret turret, float refundPercentage)
    {
        ShopItem turretShopItem = shop.GetShopItemByName(turret.name);
        GameUtils.GetPlayer().GetComponent<ResourceInventory>().Add(shop.GetCurrency(), (int)(turretShopItem.GetPrice() * refundPercentage));
    }

    private IEnumerator select (GameObject button)
    {
        yield return 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}
