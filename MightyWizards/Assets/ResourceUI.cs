using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class ResourceUI : MonoBehaviour {

    [Tooltip("The type of pickup this UI text tracks")]
    [SerializeField] private PickupData resourceType;

    private Text text;
    private int currentAmount;

	private void Awake ()
    {
        text = GetComponent<Text>();
        text.text = currentAmount.ToString();
    }

    public void AddAmount(PickupData pickup, int amount)
    {
        if (pickup == resourceType)
            currentAmount += amount;

        text.text = currentAmount.ToString();
    }
}
