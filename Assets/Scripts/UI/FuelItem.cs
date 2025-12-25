using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuelItem : MonoBehaviour
{
    public float fuelAmount = 25f;
    public int price = 50;

    public TMP_Text priceText;
    public Button buyButton;

    void Start()
    {
        priceText.text = "Price : " + price;
        buyButton.onClick.AddListener(Buy);
    }

    void Buy()
    {
        if (!GameManager.Instance.HasEnoughCoins(price)) return;

        GameManager.Instance.SpendCoins(price);
        GameManager.Instance.AddGlobalFuel(fuelAmount);
    }
}
