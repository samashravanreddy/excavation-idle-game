using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelSceneManager : MonoBehaviour
{
   public void OnMachineButtonClicked() 
   {
     SceneManager.LoadScene("Machine_Store"); // Change to your next scene name 
   } 
   public void OnBackButtonClicked() 
   {
     SceneManager.LoadScene("GameScene"); // Change to your next scene name }
   }

}