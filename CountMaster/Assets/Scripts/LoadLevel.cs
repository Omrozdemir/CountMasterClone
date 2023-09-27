using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public GameObject MenuPanel;
 public void loadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Pause()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
