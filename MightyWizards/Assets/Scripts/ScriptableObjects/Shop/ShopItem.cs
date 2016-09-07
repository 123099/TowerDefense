using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject {

    [Tooltip("The item to sell. This can be anything.")]
	[SerializeField] private Object item;
    [Tooltip("The name of the item to display in the shop")]
    [SerializeField] private string itemName;
    [Tooltip("The description of the item to display in the shop")]
    [SerializeField] private string itemDesc;
    [Tooltip("The icon to display in the shop")]
    [SerializeField] private Sprite icon;
    [Tooltip("The category in the shop this item belongs to")]
    [SerializeField] private ShopItemCategory category;
    [Tooltip("The price in the shop's currency this item sells for")]
    [SerializeField] private int price;

    public Object GetItem ()
    {
        return item;
    }

    public int GetPrice ()
    {
        return price;
    }

    public ShopItemCategory GetCategory ()
    {
        return category;
    }

    public Sprite GetIcon ()
    {
        return icon;
    }

    public string GetName ()
    {
        return itemName;
    }

    public string GetDesc ()
    {
        return itemDesc;
    }

    //Setter methods should be used on clones only
    public void SetPrice (int price)
    {
        this.price = price;
    }
}
