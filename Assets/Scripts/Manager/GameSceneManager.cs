using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    public Transform machineListParent;
    public GameObject machinePanelPrefab;

    private List<MachinePanel> activeMachines = new List<MachinePanel>();
    public int ActiveMachineCount => activeMachines.Count;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        AutoAssignContent();
        StartCoroutine(DelayedSpawn());

    }

    public float GetTotalFuelUsage()
    {
        float total = 0;
        foreach (var m in activeMachines)
            total += m.currentData.fuelPerSec;
        return total;
    }

    public void Register(MachinePanel m) => activeMachines.Add(m);
    public void UnRegister(MachinePanel m) => activeMachines.Remove(m);

    [Serializable]
    public class MachineListWrapper
    {
        public List<MachineSaveData> machines = new();
    }
    IEnumerator DelayedSpawn()
{
    yield return null; // wait one frame

    AutoAssignContent();
    LoadSavedMachines();

    if (TempPurchase.selectedMachine != null)
    {
        SpawnMachine(TempPurchase.selectedMachine);
        TempPurchase.selectedMachine = null;
        SaveMachines();
    }
}
    public void SpawnMachine(MachineData template)
{
    if (machineListParent == null)
    {
        Debug.LogError("Spawn failed: machineListParent is NULL");
        return;
    }

    MachineData data = template.Clone();

    GameObject panel = Instantiate(machinePanelPrefab, machineListParent);
    panel.transform.localScale = Vector3.one;

    MachinePanel mp = panel.GetComponent<MachinePanel>();
    mp.SetData(data);
    RecalculateTotalIncome();
    Debug.Log("Machine spawned: " + data.machineName);
}
public void RecalculateTotalIncome()
{
    float total = 0;

    foreach (var m in activeMachines)
    {
        total += m.currentData.incomePerSec;
    }

    GameManager.Instance.totalIncomePerSec = total;
    GameManager.Instance.UpdateHUD();
}


    public void SaveMachines()
    {
        var wrap = new MachineListWrapper();
        var panels = machineListParent.GetComponentsInChildren<MachinePanel>();

        for (int i = 0; i < panels.Length; i++)
            wrap.machines.Add(panels[i].GetSaveData(i));

        PlayerPrefs.SetString("machines", JsonUtility.ToJson(wrap));
        PlayerPrefs.Save();
    }

    public void LoadSavedMachines()
    {
        string json = PlayerPrefs.GetString("machines", "");
        if (string.IsNullOrEmpty(json)) return;

        var wrap = JsonUtility.FromJson<MachineListWrapper>(json);

        foreach (var s in wrap.machines)
        {
            var data = new MachineData(s.machineName, null, s.incomePerSec,
               Mathf.RoundToInt( s.fuelCapacity), Mathf.RoundToInt(s.fuelUsePerSec), 0);
            var panel = Instantiate(machinePanelPrefab, machineListParent);
            var mp = panel.GetComponent<MachinePanel>();
            mp.SetData(data, true);
            mp.level = s.level;
        }
        RecalculateTotalIncome();
    }

  void AutoAssignContent()
{
    if (machineListParent != null) return;

    Canvas canvas = FindObjectOfType<Canvas>();
    if (canvas == null)
    {
        Debug.LogError("Canvas not found in GameScene");
        return;
    }

    Transform content = canvas.transform.Find("Content");
    if (content == null)
    {
        Debug.LogError("Content not found under Canvas");
        return;
    }

    machineListParent = content;
}

}

