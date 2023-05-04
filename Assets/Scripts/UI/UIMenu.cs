using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

}
