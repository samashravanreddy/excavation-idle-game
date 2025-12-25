using System;

[Serializable]
public class MachineSaveData
{
    public string machineName;

    public int level;
    public int incomePerSec;

    public float fuelCapacity;
    public float currentFuel;
    public float fuelUsePerSec;

    public int index;   // position in list
}
