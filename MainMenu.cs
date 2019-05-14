//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Will jump between scenes using the script
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Variables created
    public GameObject howToMenuUI; //Variable for loading the how to play menu on main menu

    public Text highScoreText;

    void Start()
    {
        if (PlayerPrefs.GetString ("HighScoreName") != "")
        highScoreText.text = "The current highest balance is achieved by " + PlayerPrefs.GetString("HighScoreName") + "\n" + PlayerPrefs.GetInt("HighScore") + " BALANCE";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); //Escape button quits the game
            Debug.Log("You have aborted the mission");
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //Quit game button
        Debug.Log("Abort Mission initiated");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); //Start game button loads in main gaming level scene
    }

    public void HowToPlay() //Loads up the how to play text
    {
        howToMenuUI.SetActive(true);
    }

    public void CloseHowToPlay() //Loads the close button in how to play text
    {
        howToMenuUI.SetActive(false);
    }
}
