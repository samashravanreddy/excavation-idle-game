using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Stats")]
    public int coins = 0;
    public float totalIncomePerSec = 0f;

    [Header("Global Fuel")]
    public float globalFuel = 100f;


    [Header("UI References")]
    public TMP_Text coinsText;
    public TMP_Text incomeText;
    public TMP_Text globalFuelText;

    private void Awake()
    {
        // ❌ REMOVE AFTER TESTING
        PlayerPrefs.DeleteAll();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        TryFindUI();
        UpdateHUD();

        InvokeRepeating(nameof(AddIncome), 1f, 1f);
        InvokeRepeating(nameof(ConsumeGlobalFuel), 1f, 1f);
    }
    float fuelTimer = 0f;

void Update()
{
    if (GameSceneManager.Instance == null) return;

    fuelTimer += Time.deltaTime;
    if (fuelTimer >= 1f)
    {
        fuelTimer = 0f;
        ConsumeGlobalFuel();
        AddIncome();
    }
}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindUI();
        UpdateHUD();
    }

    void TryFindUI()
    {
        coinsText = GameObject.Find("Text_Coins")?.GetComponent<TMP_Text>() ?? coinsText;
        incomeText = GameObject.Find("Text_IncomePerSec")?.GetComponent<TMP_Text>() ?? incomeText;
        globalFuelText = GameObject.Find("Text_GlobalFuel")?.GetComponent<TMP_Text>() ?? globalFuelText;
    }

    void AddIncome()
{
    if (GameSceneManager.Instance == null) return;
    if (GameSceneManager.Instance.ActiveMachineCount == 0) return;
    if (globalFuel <= 0) return;

    coins += Mathf.RoundToInt(totalIncomePerSec);
    UpdateHUD();
    SavePlayerData();
}


   void ConsumeGlobalFuel()
{
    if (GameSceneManager.Instance == null) return;
    if (GameSceneManager.Instance.ActiveMachineCount == 0) return;

    float usage = GameSceneManager.Instance.GetTotalFuelUsage();
    if (usage <= 0) return;

    globalFuel -= usage;
    if (globalFuel < 0) globalFuel = 0;

    UpdateHUD();
    SavePlayerData();
}


    public void AddGlobalFuel(float amount)
{
    globalFuel += amount;
    UpdateHUD();
    SavePlayerData();
}


    public bool HasEnoughCoins(int amount) => coins >= amount;

    public void SpendCoins(int amount)
    {
        coins -= amount;
        if (coins < 0) coins = 0;
        UpdateHUD();
        SavePlayerData();
    }

    public void ModifyIncome(float delta)
    {
        totalIncomePerSec += delta;
        if (totalIncomePerSec < 0) totalIncomePerSec = 0;
        UpdateHUD();
        SavePlayerData();
    }

    public void UpdateHUD()
    {
        if (coinsText) coinsText.text = "Coins: " + coins;
        if (incomeText) incomeText.text = "+ " + totalIncomePerSec + "/sec";
        if (globalFuelText)
    globalFuelText.text = $"Fuel: {Mathf.Floor(globalFuel)}";

    }

    void SavePlayerData()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetFloat("income", totalIncomePerSec);
        PlayerPrefs.SetFloat("globalFuel", globalFuel);
        PlayerPrefs.Save();
    }

    void LoadPlayerData()
    {
        coins = PlayerPrefs.GetInt("coins", 10000);
        totalIncomePerSec = PlayerPrefs.GetFloat("income", 0);
        globalFuel = PlayerPrefs.GetFloat("globalFuel",100f);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
