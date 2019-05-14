//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldScroller : MonoBehaviour
{
    //Variables created for scrolling speed:
    public float scrollSpeed; //Variable to control the speed at which the world scrolls
    public float worldLength; //Variable to control when the level ends or lenght of map has reached the end

    //Fixed Update is called at every fixed cycle
    void FixedUpdate()
    {
        //Below is to control the speed at which Z axis moves underneath the player
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - scrollSpeed);

        //Below creates an end state to the speed of the scrolling
        if (transform.position.z <= -200)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndCredits");
        }
    }
}
