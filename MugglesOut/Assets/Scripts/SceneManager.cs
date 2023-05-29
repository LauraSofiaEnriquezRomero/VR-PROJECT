using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{

    public void ChangeScene(int sceneIndex)
    {
        Debug.Log("va cambiando");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}


