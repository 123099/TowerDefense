﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
public class ShopItemUI : MonoBehaviour {

    [Tooltip("The image component displaying the icon of this item")]
    [SerializeField] private Image icon;
    [Tooltip("Text displaying the stack size of the item")]
    [SerializeField] private Text stackSizeText;
    [Tooltip("Text displaying the price of the item")]
    [SerializeField] private Text priceTag;

	public void SetupShopItem(ShopItem shopItem)
    {
        icon.sprite = shopItem.GetIcon();
        stackSizeText.text = "1";
        priceTag.text = shopItem.GetPrice().ToString();

        SpriteState spriteState = GetComponent<Button>().spriteState;
        spriteState.highlightedSprite = shopItem.GetHighlightIcon();
        GetComponent<Button>().spriteState = spriteState;
    }
}
