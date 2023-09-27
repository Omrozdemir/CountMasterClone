using UnityEngine;
using UnityEngine.SceneManagement;
public class EndTrigger : MonoBehaviour
{
    public GameManager GameManager;
    void OnTriggerEnter()
    {
        
       GameManager.CompleteLevel();
    }
}
