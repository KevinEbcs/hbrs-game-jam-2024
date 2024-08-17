using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject levelLoaderObj;
        private LevelLoader _levelLoader;

        public void Start()
        {
            _levelLoader = LevelLoader.GetInstance();
        }
        
        // Quit Game
        public void Quit(){
            Application.Quit();
            Debug.Log("Player has quit the game.");
        }

        public void Play()
        {
            //Cursor.visible = false;
            if (_levelLoader)
            {
                _levelLoader.LoadNextLevel("Prototype1");
            }
        }
    }
}
