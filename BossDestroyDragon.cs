//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyDragon : MonoBehaviour
{
    //Variables created below
    public GameObject dragonExplosion;
    public GameObject playerExplosion;

    //Variables created for score system
    public int scoreValue;



    private GameManager GM;

    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            GM = gameControllerObject.GetComponent<GameManager>();
        }
        if (GM == null)
        {
            Debug.Log("Cannot find GameManager script");
        }


    }


    //OnTriggerEnter - lazer enters the collider of dragon, the lazer needs to disappear and the dragon must be destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return; //If the above is true, then it hits return and stops the entire function below from running (boundary will not disappear with objects inside)
        }

        //Below instantiates the player shooting the dragon or both dragon and ship are destroyed
        Instantiate(dragonExplosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            return;
        }

        GM.AddScore(scoreValue);



        //If any other object, not containing the tag name "boundary" the below function will take place (lazer and dragon destruction)
        Destroy(other.gameObject); //Destroys any collider that enters the dragon's collider
        Destroy(gameObject); //Destroys the game object that contains this script (dragon enemy)

 
    }
}
