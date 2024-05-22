using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistory : MonoBehaviour
{
    private static SceneHistory instance;
    private Stack<int> sceneStack = new Stack<int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneHistory Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SceneHistory");
                instance = obj.AddComponent<SceneHistory>();
            }
            return instance;
        }
    }

    public void LoadScene(string sceneName)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneStack.Push(currentSceneIndex);
        SceneManager.LoadScene(sceneName);
    }

    public void Back()
    {
        if (sceneStack.Count > 0)
        {
            int previousSceneIndex = sceneStack.Pop();
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogError("SceneHistory: sceneStack is empty");
            SceneManager.LoadScene("TitleScene");
        }
    }
}
