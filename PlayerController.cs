//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Custom classes need to be serialized into Unity in order for Unity to recognize the custom class functions
public class Boundary //custom class is created to contain boundary values - this declutters Unity Inspector
{
    public float xMin, xMax, zMin, zMax; //Unity will see all the boundary positions of X and Z as float values
}

public class PlayerController : MonoBehaviour
{
    //Below are variable names created that are pulled into Unity by the scripts
    public Rigidbody rb; //rb variable for the playable ship
    public float speed; //variable for speed multiplier of playable ship movement
    public Boundary boundary; //boundary variable is made referenced to for script to call it
    public float tilt; //variable for tilt movement of playable ship

    public GameObject lazers; //instantiate lazers
    public Transform lazerSpawn; //instantiate lazer position and rotation
    public float fireRate; //firing rate of lazers
    private float nextFire; //time intervals between fire rate - can only be changed in the script and not public in the Inspector

    //Variable for access to GM and DamageScript
    public GameManager GM;
    public DamageScript damageScript;
    public float damageTotal;

    public float healthDecrease;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>(); //rb variable is now associated with the playable ship at the start of the game
    }

    void Update()
    {
        //Will need 2 conditions to be true in the below if statement
        if (Input.GetButton("Jump") && Time.time > nextFire) //Condition is that each time the player presses the "jump" or space bar button the lazer will spawn and instantiate
        {
            //Below is used to check if fire rate is greater than or smaller than what the rate was previously
            nextFire = Time.time + fireRate;
            //Below instantiates each lazer when "jump" is used by player
            Instantiate(lazers, lazerSpawn.position, lazerSpawn.rotation); //Instantiate requires a game object, a position and rotation
        }

    }

    //FixedUpdate is called to run physics at every physics cycle or as set intervals
    public void FixedUpdate()
    {
        //Below gets direction input from Unity default movement settings - move ship backwards and forwards (not up and down on Y axis for this game)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Below controls the movement and speed of the playable ship. We need Vector3 as physics for left/right/back/forwards
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Vector3 float values are x,y,z. We do not need to go up and down, so Y is zero

        //Below controls the speed of the movement
        rb.velocity = movement * speed;

        //Below limits the playable area and boundary variable is also called in by the script
        rb.position = new Vector3 //Mathf is a math function in C#, Clamp gives min/max values
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), //X min/max boundary position
                6.0f, //Y default height will be zero
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax) //Z min/max boundary position
            );

        //Below controls the tilt of the playable ship
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt); //Euler has x/y/z float values, X and Y are not used, Z is where tilt occurs, must -tilt to get it in opposite direction
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) //When player collides with enemies = damage on player
        {
            damageTotal = damageScript.damageValue;
            GM.DamageReceived(damageTotal);
        }

        if (other.CompareTag("DeathGem")) //When player collides with gems = power downs (takes more health)
        {
            Destroy(other.gameObject);
            healthDecrease = damageScript.damageValue;
            GM.DamageReceived(healthDecrease);
        }

    }
}
