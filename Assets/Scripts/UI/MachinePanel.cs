using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MachinePanel : MonoBehaviour
{
    public MachineData currentData;

    public TMP_Text nameText, incomeText, fuelText, levelText;
    public Button upgradeButton, deleteButton;
    public Slider fuelBar;

    public int level = 1;

    private float visualFuel;

    void OnEnable()
    {
        GameSceneManager.Instance.Register(this);
        StartCoroutine(FuelUIRoutine());
    }

    void OnDisable()
    {
        GameSceneManager.Instance.UnRegister(this);
    }

    public void SetData(MachineData data, bool loaded = false)
    {
        currentData = data;
        level = data.upgradeLevel;

        visualFuel = data.fuelCapacity;
        fuelBar.maxValue = data.fuelCapacity;
        fuelBar.value = visualFuel;

        nameText.text = data.machineName;
        levelText.text = "Level: " + level;

        upgradeButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();

        upgradeButton.onClick.AddListener(UpgradeMachine);
        deleteButton.onClick.AddListener(DeleteMachine);
    }

    IEnumerator FuelUIRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (GameManager.Instance.globalFuel <= 0)
            {
                incomeText.text = "+0 (No Fuel)";
                continue;
            }

            visualFuel -= currentData.fuelPerSec;
            if (visualFuel <= 0) visualFuel = currentData.fuelCapacity;

            fuelBar.value = visualFuel;
            fuelText.text = $"{Mathf.Floor(visualFuel)} / {currentData.fuelCapacity}";
            incomeText.text = "+ " + currentData.incomePerSec + "/sec";
        }
    }

    void UpgradeMachine()
    {
        int cost = 100 * level;
        if (!GameManager.Instance.HasEnoughCoins(cost)) return;

        GameManager.Instance.ModifyIncome(-currentData.incomePerSec);
        GameManager.Instance.SpendCoins(cost);

        level++;
        currentData.upgradeLevel = level;
        currentData.incomePerSec += 2;
        currentData.fuelCapacity += 5;

        fuelBar.maxValue = currentData.fuelCapacity;
        visualFuel = currentData.fuelCapacity;

        GameManager.Instance.ModifyIncome(currentData.incomePerSec);
        levelText.text = "Level: " + level;
        GameSceneManager.Instance.RecalculateTotalIncome();
        GameSceneManager.Instance.SaveMachines();
    }

    void DeleteMachine()
    {
        GameManager.Instance.ModifyIncome(-currentData.incomePerSec);
        Destroy(gameObject);
        GameSceneManager.Instance.RecalculateTotalIncome();
        GameSceneManager.Instance.SaveMachines();
    }

    public MachineSaveData GetSaveData(int index)
    {
        return new MachineSaveData
        {
            machineName = currentData.machineName,
            level = level,
            incomePerSec = currentData.incomePerSec,
            fuelCapacity = currentData.fuelCapacity,
            fuelUsePerSec = currentData.fuelPerSec,
            index = index
        };
    }
}
