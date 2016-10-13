using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - Peter S. Wages
 **/

public class PlayerController : MonoBehaviour {
    private int health = 100;
    private int damagePerHit = 25;
    private int pointsLostPerHit = -50;
    private FPSController controller;
    [SerializeField]
    private Slider healthBar;

    void Start()
    {
        controller = FindObjectOfType<FPSController>();
    }

    public void PlayerHit()
    {
		if (health > 0) {
			health = health - damagePerHit;
			healthBar.value = health;
			controller.UpdateScore (pointsLostPerHit);
		} else {
			controller.TriggerLoss ();
		}
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Bullet")
        {
            PlayerHit();
			Destroy (collidedWith);
        }
    }
}
