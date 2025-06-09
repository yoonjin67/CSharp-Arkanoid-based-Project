using UnityEngine;
using UnityEngine.SceneManagement; 

public class BlocksManager : MonoBehaviour
{
    public SharedIntVariable counter;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void LoadSpecificSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSpecificSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void Awake()
    {
        this.counter.value = 51;
    }

    void Update()
    {
        var blocksLeft = counter.value;
        Debug.Log($"Blocks Left: {blocksLeft}");
        if (blocksLeft == 0)
        {
            LoadNextScene();
        }
    }
}
