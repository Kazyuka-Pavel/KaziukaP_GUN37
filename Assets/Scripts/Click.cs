using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public void Click1()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
