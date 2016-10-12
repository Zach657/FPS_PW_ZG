using UnityEngine;
using System.Collections;

/*
 * @author James Greenwell
 * 
 * This class controls the enemy interactions with the environment, including interactions with the player
*/ 

public class EnemyController : MonoBehaviour {
	[SerializeField] private int health = 1;
	[SerializeField] private float xVel = 0;
	[SerializeField] private float zVel = 0;
	[SerializeField] private float distance = 0;

	private float currentDistance = 0;

	//these two variables make it possible to set negative velocities without having to worry about it affecting 
	//distance (calculated by adding the two sides of the triangle rather than the hypotenuse)
	private float adjX = 0;
	private float adjZ = 0;

	//boolean on whether or not the enemy is engaging the player
	private bool firing = false;

	//to simulate gradual turning for enemies
	private bool rotating = false;
	private int rotated = 0;
	// Use this for initialization
	void Start () {
		adjX = Mathf.Abs (xVel);
		adjZ = Mathf.Abs (zVel);
	}
	
	// Responsible for enemy movement
	void FixedUpdate () {
		SearchForPlayer ();
		if (!firing) {
			if (!rotating) {
				Vector3 updatePos = this.transform.position;
				currentDistance += adjX + adjZ;
				updatePos.x = updatePos.x + xVel;
				updatePos.z = updatePos.z + zVel;
				this.transform.position = updatePos;
				if (currentDistance >= distance && adjX >= 0 && adjZ >= 0) {
					xVel = -xVel;
					zVel = -zVel;
					adjX = -adjX;
					adjZ = -adjZ;
					rotating = true;
				}

				if (currentDistance <= 0 && adjX <= 0 && adjZ <= 0) {
					xVel = -xVel;
					zVel = -zVel;
					adjX = -adjX;
					adjZ = -adjZ;
					rotating = true;
				}
			} else {
				if (rotated < 180) {
					this.transform.Rotate (0, 2, 0);
					rotated += 2;

				} else {
					rotated = 0;
					rotating = false;
				}
			}
		} else {
			EngagePlayer ();
		}
	}
	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		// This determines whether or not the enemy was hit by a bullet
		if (collidedWith.tag == "Bullet") {
			health = health - 1;
			if (health == 0) {
				
			}
		}
	}
	void EngagePlayer(){
		
	}

	void SearchForPlayer(){
		
	}
}
