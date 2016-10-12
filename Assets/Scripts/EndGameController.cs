using UnityEngine;
using System.Collections;
/*
 * @ author Peter Wages
*/
public class EndGameController : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        Debug.Log("COLLISION");
        if (collidedWith.tag == "Player")
        {
            Debug.Log("Player has won");
            // TriggerMazeCompletion();
        }
    }
}
