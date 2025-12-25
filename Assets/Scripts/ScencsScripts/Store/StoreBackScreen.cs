using UnityEngine;
using UnityEngine.SceneManagement;
public class StoreBackScreen : MonoBehaviour
{
   public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("GameScene"); // Change to your next scene name
    }
}
