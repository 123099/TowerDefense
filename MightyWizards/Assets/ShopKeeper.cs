using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour {

    [Tooltip("The shop this keeper is keeping")]
    [SerializeField] private Shop shop;

    [Tooltip("Staff button template")]
    [SerializeField] private ShopItemUI staffTemplate;
    [Tooltip("Turret button template")]
    [SerializeField] private ShopItemUI turretTemplate;

    private void OnEnable ()
    {
        SetupCategory(ShopItemCategory.Staff, staffTemplate);
        SetupCategory(ShopItemCategory.Turret, turretTemplate);
    }

    private void SetupCategory(ShopItemCategory category, ShopItemUI template)
    {
        ShopItem[] items = shop.GetItemsOfCategory(category);
        for(int i = 0; i < items.Length; ++i)
        {
            ShopItem item = items[i];

            ShopItemUI shopItemUI = Instantiate(template, template.transform.parent) as ShopItemUI;
            shopItemUI.SetupShopItem(item);

            RectTransform shopItemTransform = shopItemUI.GetComponent<RectTransform>();
            Vector2 anchorMin = shopItemTransform.anchorMin;
            Vector2 anchorMax = shopItemTransform.anchorMax;

            anchorMin.x *= i + 1;
            anchorMax.x *= i + 1;

            shopItemTransform.anchorMin = anchorMin;
            shopItemTransform.anchorMax = anchorMax;

            Button itemButton = shopItemUI.GetComponent<Button>();
            itemButton.onClick.AddListener(() => PurchaseItem(shop.IndexOf(item)));
        }

        template.gameObject.SetActive(false);
    }

	public void PurchaseItem(int index)
    {
        Object[] purchasedItems = shop.Purchase(GameUtils.GetPlayer().GetComponent<ResourceInventory>(), index);
        
        foreach(Object purchasedItem in purchasedItems)
        {
            if(purchasedItem is Staff)
            {
                GameUtils.GetPlayer().SetStaff(purchasedItem as Staff);
            }
            else if(purchasedItem is Turret)
            {

            }
        }
    }
}
