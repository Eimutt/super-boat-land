using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(string sceneName, bool saveCurrent)
    {
        if (saveCurrent)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] gameObjects = scene.GetRootGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }

        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName && scene.isLoaded)
                {
                    GameObject[] gameObjects = scene.GetRootGameObjects();
                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.SetActive(true);
                    }
                    SceneManager.SetActiveScene(scene);
                }
            }
            SceneManager.UnloadSceneAsync(currentScene);
            
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
}
