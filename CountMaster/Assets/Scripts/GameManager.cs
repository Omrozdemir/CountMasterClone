using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public GameObject RetryCanvas;
    public GameObject completeLevelUI;
    bool gameHasEnded = false;

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void endGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Retry", restartDelay);
        }
    }
    public void Retry()
    {
        RetryCanvas.SetActive(true);
    }
}
