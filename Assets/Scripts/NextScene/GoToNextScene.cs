using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            LoadNextScene();
        }
        UpdateForMobile();
    }
    void UpdateForMobile()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            LoadNextScene();
        }
    }

}
