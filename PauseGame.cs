//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    //Variables
    public static bool gameIsPaused = false; //Static - at later stage can gain access to see the state (data) without referencing the entire script
    public GameObject PauseMenuUI; //Activate the UI canvas/pause menu

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseTheGame();
            }
        }
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseTheGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Can make it 1f, which causes time to slow down but not at stand still, so player cannot stay in pause menu for too long
        gameIsPaused = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
