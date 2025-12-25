using UnityEngine;
using UnityEngine.SceneManagement;
public class Game1SceneManager : MonoBehaviour
{
   public void OnStoreButtonClicked()
    { 
        SceneManager.LoadScene("Store"); // Change to your next scene name 
    } 
     public void OnMenuButtonClicked() 
    { 
        SceneManager.LoadScene("StartScene"); // Change to your next scene name
    }
}
