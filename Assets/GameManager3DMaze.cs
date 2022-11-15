using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3DMaze : MonoBehaviour
{
    [SerializeField] Hole hole;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverText;
    // [SerializeField] PhoneGravity phoneGravity;
    bool isGameOver = false;

    private void Update()
    {
        if (hole.Entered && gameOverPanel.activeInHierarchy == false)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Congratulations" + "\n" + "Got Next Levels";
            if (isGameOver == false)
            {
                isGameOver = true;
                // phoneGravity.enabled = false;
                Time.timeScale = 0;
            }

        }
    }
    public void passLevels()
    {
        int currentLevels = SceneManager.GetActiveScene().buildIndex;
        if (currentLevels >= PlayerPrefs.GetInt("levelsUnlocked", 1))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevels + 1);
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
        resumeGame();
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
        resumeGame();
    }

    public void PlayNext()
    {
        Debug.Log("PlayNext");
        SceneLoader.LoadNextLevel();
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

}
