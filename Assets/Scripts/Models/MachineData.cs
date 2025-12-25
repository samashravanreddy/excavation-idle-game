using UnityEngine;

[System.Serializable]
public class MachineData
{
    public string machineName;
    public int incomePerSec;
    public int fuelCapacity;
    public int fuelPerSec;
    public int price;
    public int upgradeLevel = 1;
    public Sprite icon;

    public MachineData(string name, Sprite icon, int income, int fuelCap, int fuelUse, int price)
    {
        machineName = name;
        this.icon = icon;
        incomePerSec = income;
        fuelCapacity = fuelCap;
        fuelPerSec = fuelUse;
        this.price = price;
    }

    public MachineData Clone()
    {
        return new MachineData(machineName, icon, incomePerSec, fuelCapacity, fuelPerSec, price)
        {
            upgradeLevel = upgradeLevel
        };
    }
}
