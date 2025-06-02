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

    void Update()
    {
        var blocksLeft = counter.value;
        if (blocksLeft == 0)
        {
            LoadNextScene();
        }
    }
}
