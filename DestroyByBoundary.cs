//Tao Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    //Below condition is to prevent the main ship from being destroyed if it touches the boundary, but also if lazers hit the boundary it will be destroyed and disappear from the heirarchy.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lazers"))
        {
            Destroy(other.gameObject);
        }
    }
}
