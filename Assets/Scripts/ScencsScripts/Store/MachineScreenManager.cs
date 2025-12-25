using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineScreenManager : MonoBehaviour
{
    public void OnMachineButtonClicked()
    {
        SceneManager.LoadScene("Machine_Store"); // Change to your next scene name
    }
}
