using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter S. Wages
 **/
public class EndGameController : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Player")
        {
            Debug.Log("Player has won");
            // TriggerMazeCompletion();
        }
    }
}
