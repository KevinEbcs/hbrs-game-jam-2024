using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        // Quit Game
        public void Quit(){
            Application.Quit();
            Debug.Log("Player has quit the game.");
        }

        public void Play()
        {
            SceneManager.LoadScene("Prototype1");
        }
    }
}
