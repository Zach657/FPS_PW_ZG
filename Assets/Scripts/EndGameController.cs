using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - Peter S. Wages
 **/
public class EndGameController : MonoBehaviour {
    private FPSController controller;

    void Start()
    {
        controller = FindObjectOfType<FPSController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Player")
        {
            controller.TriggerWin();
        }
    }
}