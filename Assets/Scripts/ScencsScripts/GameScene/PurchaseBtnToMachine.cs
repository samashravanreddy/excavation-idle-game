using UnityEngine;
using UnityEngine.SceneManagement;

public class PurchaseBtnToMachine : MonoBehaviour
{
    public void OnPurchasedButtonClicked()
    {
        SceneManager.LoadScene("Machine_Store"); // Change to your next scene name
    }
}
