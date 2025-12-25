using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineSceneManager : MonoBehaviour
{
    public void OnFuelButtonClicked()
    {
        SceneManager.LoadScene("Fuel_Store"); // Change to your next scene name
    }
    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("GameScene"); // Change to your next scene name
    }
}
