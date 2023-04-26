using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads a Scene.
    /// </summary>
    /// <param name="scene">The name of the scene.</param>
    /// <param name="callback">The Action we want to execute after the scene is loaded.</param>
    public void LoadScene(string scene, UnityAction callback = null)
    {
        SceneManager.LoadScene(scene);

        if (callback != null)
        {
            Scene toLoad = SceneManager.GetSceneByName(scene);
            SceneManager.sceneLoaded += delegate { callback.Invoke(); };
        }
    }
   /// <summary>
   /// Loads a Scene.
   /// </summary>
   /// <param name="scene">The index of the scene.</param>
   /// <param name="callback">The Action we want to execute after the scene is loaded.</param>
    public void LoadScene(int scene, UnityAction callback = null)
    {
        SceneManager.LoadScene(scene);

        if (callback != null)
        {
            Scene toLoad = SceneManager.GetSceneByBuildIndex(scene);
            SceneManager.sceneLoaded += delegate { callback.Invoke(); };
        }
    }
    /// <summary>
    /// Loads a Game Level (id starts at 1).
    /// </summary>
    /// <param name="level">The id of the level we want to load (starts at 1)</param>
    public void LoadLevel(int level)
    {
        string levelName = string.Format("Level_{0}", level);
        LoadScene(levelName);
    }
}
