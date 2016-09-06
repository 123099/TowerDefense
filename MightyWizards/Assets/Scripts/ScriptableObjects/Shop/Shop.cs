using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Shop/Shop")]
public class Shop : ScriptableObject {

    [Tooltip("The currency of the shop")]
    [SerializeField] private PickupData currency;
    [Tooltip("A list of items to sell")]
	[SerializeField] private ShopItem[] items;

    public PickupData GetCurrency ()
    {
        return currency;
    }

    public int GetItemCount ()
    {
        return items.Length;
    }

    public ShopItem GetItemAt(int index)
    {
        return items[index];
    }

    public int IndexOf (ShopItem item)
    {
        for (int i = 0; i < items.Length; ++i)
            if (items[i] == item)
                return i;

        return -1;
    }

    public ShopItem[] GetItemsOfCategory (ShopItemCategory category)
    {
        List<ShopItem> items = new List<ShopItem>();
        foreach (ShopItem item in this.items)
            if (item.GetCategory() == category)
                items.Add(item);

        return items.ToArray();
    }

    public Object Purchase(ResourceInventory resourceInventory, int itemIndex)
    {
        return Purchase(resourceInventory, GetItemAt(itemIndex));
    }

    public Object Purchase(ResourceInventory resourceInventory, ShopItem item)
    {
        //Prepare order
        Object purchasedItem = null;

        //Check funds
        if (!resourceInventory.Has(currency, item.GetPrice())) return purchasedItem;

        //Retrieve items from warehouse and pack them
        purchasedItem = Instantiate(item.GetItem());

        //Label item for shipping
        purchasedItem.name = item.GetItem().name;

        //Withdraw funds
        resourceInventory.Add(currency, -item.GetPrice());

        //Ship products
        return purchasedItem;
    }

}
