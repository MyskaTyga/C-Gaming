//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    //Variables
    public float lifeTime; //Adds a life time delay


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); //Destroys the game object that holds this script after a certain life time, so that it does not bog down the hierarchy and memory
    }

}
