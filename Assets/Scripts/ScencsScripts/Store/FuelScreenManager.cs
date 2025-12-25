using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelScreenManager : MonoBehaviour
{
    public void OnFuelButtonClicked()
    {
        SceneManager.LoadScene("Fuel_Store"); // Change to your next scene name
    }
}
