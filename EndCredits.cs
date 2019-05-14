using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Will jump between scenes using the script
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    //Variables created
  

    public Text highScoreText;
    public int score;
    public InputField highScoreStringInputField;

    void Start()
    {
        if (PlayerPrefs.GetString("HighScoreName") != "")
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

        int highScore = PlayerPrefs.GetInt("HighScore");

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Highest balance achieved is: " + score;
            highScoreStringInputField.gameObject.SetActive(true);

        }
        else
        {
            highScoreText.text = "Current highest balance achieved is " + PlayerPrefs.GetInt("HighScore") + " by Venerable: " + PlayerPrefs.GetString("HighScoreName");

        }
    }

    public void NewHighScore()
    {
        string highScoreName = highScoreStringInputField.text;
        PlayerPrefs.SetString("HighScoreName", highScoreName);
        highScoreStringInputField.gameObject.SetActive(false);
        highScoreText.text = "Thank you for helping us save our future " + highScoreName;

    }

    public void QuitGame()
    {
        Application.Quit(); //Quit game button
        Debug.Log("Abort Mission initiated");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu"); //Start game button loads in main gaming level scene
    }


}
