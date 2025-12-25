using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineStoreItem : MonoBehaviour
{
    public MachineData machineData; // set prototype in inspector

    public TMP_Text nameText;
    public TMP_Text infoText;
    public TMP_Text priceText;
    public Button buyButton;

    void Start()
    {
        if (machineData != null)
        {
            if (nameText != null) nameText.text = machineData.machineName;
            if (infoText != null) infoText.text =
                "Capacity: " + machineData.fuelCapacity + "\n" +
                "Fuel Usage: " + machineData.fuelPerSec + "/sec\n" +
                "+" + machineData.incomePerSec + "/sec";
            if (priceText != null) priceText.text = "Price : " + machineData.price;
        }

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(Buy);
        }
    }

    public void Buy()
{
    if (!GameManager.Instance.HasEnoughCoins(machineData.price))
        return;

    GameManager.Instance.SpendCoins(machineData.price);

    // Save selected machine temporarily
    TempPurchase.selectedMachine = machineData;

    // Load GameScene
    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
}


}
