using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ShopKeeper : MonoBehaviour {

    [Tooltip("The shop this keeper is keeping")]
    [SerializeField] private Shop shop;

    [Tooltip("The text area for the shop. This displays important messages")]
    [SerializeField] private Text messages;
    [Tooltip("The text area for the selected item description.")]
    [SerializeField] private Text itemDescription;

    [Tooltip("Color for successful purchase")]
    [SerializeField] private Color purchaseColor;
    [Tooltip("Color for no funds")]
    [SerializeField] private Color fundsColor;
    [Tooltip("Color for item name")]
    [SerializeField] private Color itemColor;

    [Tooltip("Staff button template")]
    [SerializeField] private ShopItemUI staffTemplate;
    [Tooltip("Turret button template")]
    [SerializeField] private ShopItemUI turretTemplate;

    [Tooltip("The turret placing system to call when buying turrets")]
    [SerializeField] private TurretPlacer turretPlacer;

    private List<GameObject> spawnedButtons;

    private void Awake ()
    {
        shop = shop.Clone();
    }

    private void OnEnable ()
    {
        spawnedButtons = new List<GameObject>();

        SetupCategory(ShopItemCategory.Staff, staffTemplate);
        SetupCategory(ShopItemCategory.Turret, turretTemplate);

        if(spawnedButtons.Count > 0)
            StartCoroutine(select(spawnedButtons[0]));
    }

    private void OnDisable ()
    {
        foreach (GameObject obj in spawnedButtons)
            Destroy(obj);
    }

    private void SetupCategory(ShopItemCategory category, ShopItemUI template)
    {
        template.gameObject.SetActive(false);

        ShopItem[] items = shop.GetItemsOfCategory(category);
        for(int i = 0; i < items.Length; ++i)
        {
            ShopItem item = items[i];

            ShopItemUI shopItemUI = Instantiate(template, template.transform.parent) as ShopItemUI;
            shopItemUI.SetupShopItem(item);

            shopItemUI.gameObject.SetActive(true);

            RectTransform shopItemTransform = shopItemUI.GetComponent<RectTransform>();
            Vector2 anchorMin = shopItemTransform.anchorMin;
            Vector2 anchorMax = shopItemTransform.anchorMax;

            anchorMin.x *= 2 * i + 1;
            anchorMax.x *= 2 * i + 1;

            shopItemTransform.anchorMin = anchorMin;
            shopItemTransform.anchorMax = anchorMax;

            shopItemTransform.localScale = Vector3.one;

            Button itemButton = shopItemUI.GetComponent<Button>();
            itemButton.onClick.AddListener(() => PurchaseItem(item, shopItemUI));

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => DisplaySelectedItem(item));

            itemButton.GetComponent<EventTrigger>().triggers.Add(entry);

            spawnedButtons.Add(shopItemUI.gameObject);
        }
    }

	public void PurchaseItem(ShopItem shopItem, ShopItemUI shopItemUI)
    {
        Object purchasedItem = shop.Purchase(GameUtils.GetPlayer().GetComponent<ResourceInventory>(), shopItem);
        DisplayPurchaseMessage(purchasedItem != null);

        if(purchasedItem is Staff)
        {
            GameUtils.GetPlayer().SetStaff(purchasedItem as Staff);
            shopItem.SetPrice(0);
            shopItemUI.SetupShopItem(shopItem);
        }
        else if(purchasedItem is GameObject)
        {
            GameObject purchasedGameObject = purchasedItem as GameObject;
            if (purchasedGameObject.GetComponent<Turret>())
            {
                turretPlacer.Initialize(shop, purchasedGameObject.GetComponent<Turret>());
                turretPlacer.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private void DisplaySelectedItem(ShopItem item)
    {
        messages.text = item.GetName();
        messages.color = itemColor;

        itemDescription.text = item.GetDesc();
    }

    private void DisplayPurchaseMessage(bool success)
    {
        if (!success)
        {
            messages.text = "Insufficient funds!";
            messages.color = fundsColor;
        }
        else
        {
            messages.text = "Purchased successfully!";
            messages.color = purchaseColor;
        }
    }

    private IEnumerator select (GameObject button)
    {
        yield return 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void HideSpeechBubble ()
    {
        messages.text = "";
        itemDescription.text = "";
    }
}
