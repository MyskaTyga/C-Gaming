//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Access to Image UI in script
using UnityEngine.SceneManagement; //Access to scenes

public class GameManager : MonoBehaviour
{
    //Variables created
    public GameObject enemyDragons;

    public GameObject healthPowerDown;
    public GameObject healthPowerUp;

    //Variables for Health System:
    public Image healthBar; //Access to healthbar
    public float playerHealth; //Determine how much of health remains
    public GameObject playerShip; //Need to access the player explosions and destruction of the player ship
    public GameObject playerExplosion;

    //Need space to spawn the enemy dragons at random, so Vector 3 is used to determine place and rotation
    public Vector3 dragonSpawnsValues;
    public int dragonCount, dragonStart, dragonSpawnDelay, dragonWaveDelay; //Counts how many dragons are to be spawned in the wave; waits for a number of seconds before spawning; delays the dragons in a spawn wave to make it appear intermittently; delays the wave spawns



    //Variables created for GameOver UI:
    public bool gameIsOver;
    public GameObject GameOverUI; //Access to Game Over UI canvas in Unity;

    //Variables created for scoring system
    public Text scoreText;
    private int score;
    public Text highScoreText;
    public InputField highScoreStringInputField;
    


    // Start is called before the first frame update
    void Start()
    {
        
        score = 0; //Score is set to 0 at start of game
        UpdateScore(); //Sets the score text to the starting value

        StartCoroutine(SpawnWave()); //need to change the way C# calls up this SpawnWave and call up the IEnumerator function instead

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            EndTheGame();
        }
        else
        {
            ResumeTheGame();
        }

    }

    //Below is the function for new score values
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }


    //Below updates score during the game
    void UpdateScore()
    {
        scoreText.text = "BALANCE: " + score;
    }


    //Below EndGame and ResumeGame functions are to end the level if player dies and bring up the GameOverUI panel
    void EndTheGame()
    {
        gameIsOver = true;
        GameOverUI.SetActive(true);

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

    void ResumeTheGame()
    {
        gameIsOver = false;
        GameOverUI.SetActive(false);
    }
   
    //Below is the QuitMission and TryAgain buttons for the GameOverUI panel
    public void TryAgain()
    {
        SceneManager.LoadScene("MainScene"); //Reloads the scene/level from the beginning
    }
    
    public void QuitMission()
    {
        Application.Quit(); //The mission ends when player clicks quit button
        SceneManager.LoadScene("MainMenu"); //Loads the Main Menu scene
    }


    //Using coroutine function below to pause and play the game without changing the flow, so no longer a void function
    IEnumerator SpawnWave() //waves will be repeatable
    {
        //Create breather for player by delaying spawns
        yield return new WaitForSeconds(dragonStart); //Waits for a number of seconds before spawning the waves of dragons
        //Need to encapsulate our current "for" loop inside another loop to make waves continously appear at random
        while (true) //Whatever loops runs inside this is continous or forever loops, while the code inside the "for" condition runs for a number of times we set it as
        {
            //Want to spawn numerous rockets, the below code needs to be looped to create random waves
            for (int i = 0; i < dragonCount; i++) //i = is an incrementor, so i > counts the rockets up to a certain amount and stops the spawn of more rockets, i++ = i + 1
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-dragonSpawnsValues.x, dragonSpawnsValues.x), dragonSpawnsValues.y, dragonSpawnsValues.z); //determines the random position from left to right
                Quaternion spawnRotation = Quaternion.identity; //Identity gives a 0 rotation value to prevent spinning or rotating of game objects
                Instantiate(enemyDragons, spawnPosition, spawnRotation);

                //yield to delay the spawns even more, otherwise dragons come in lines rather than intermittently
                yield return new WaitForSeconds(dragonSpawnDelay);
            }
            yield return new WaitForSeconds(dragonWaveDelay); //delays the wave spawns
        }


    }

    public void DamageReceived(float damage)
    {
        playerHealth = playerHealth - damage; //This is to get the health value called
        healthBar.fillAmount = playerHealth; //Updates the health bar

        if (playerHealth <= 0)
        {

            Instantiate(playerExplosion, playerShip.transform.position, playerShip.transform.rotation); //Instantiate the explosion at the player ship's location

            EndTheGame();
            Destroy(playerShip); //Destroy the player ship if health reaches 0
        }


 
    }

}
