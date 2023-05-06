using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip victorySound;

    private void Awake()
    {
        //gameOverScreen.SetActive(false);
        //victoryScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        SoundManager.instance.PlaySound(victorySound);
    }

    public void Checkpoint()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
    
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        var index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
