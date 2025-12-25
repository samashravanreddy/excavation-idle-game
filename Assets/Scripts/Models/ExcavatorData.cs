using UnityEngine;

public class ExcavatorData : MonoBehaviour
{
    public string name;
    public int level;
    public float capacity; 
    public float fuelUsage; 
    public int incomePerSecond;
    public int price;

    public ExcavatorData(string name, float capacity, float fuelUsage, int income, int price)
    {
        this.name = name;
        this.capacity = capacity;
        this.fuelUsage = fuelUsage;
        this.incomePerSecond = income;
        this.price = price;
        this.level = 1;
        this.price = price;
    }
}

