using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject crossfade;
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime;
    
        private Animator _crossfadeAnim;
        private static GameObject _instance;
    
        // Start is called before the first frame update
        void Start()
        {
            if (_instance == null)
            {
                _instance = gameObject;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            _crossfadeAnim = crossfade.GetComponent<Animator>();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            transition.SetTrigger("Load");
        }
        
        public void LoadNextLevel(string sceneName, int transId = 0)
        {
            if (transId == 0)
            {
                StartCoroutine(LoadLevel(sceneName));
            }
            else
            {
                StartCoroutine(LoadLevel("TransitionText"));
            }
        }
    
        IEnumerator LoadLevel(string levelName)
        {
            crossfade.SetActive(true);
            //Play animation
            transition.SetTrigger("Start");
            //Wait
            yield return new WaitForSeconds(transitionTime);
            //Load Scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
