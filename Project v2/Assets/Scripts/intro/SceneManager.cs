using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    void Start() {
        Invoke("LoadScene", 2f); // Llama al método CargarEscena después de 5 segundos
    }

    public void ChangeScene(string Level_1)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

