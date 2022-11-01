using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

    }
}
