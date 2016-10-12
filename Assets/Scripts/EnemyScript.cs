using UnityEngine;
using System.Collections;

/*
 * @author James Greenwell
 * 
 * This class controls the enemy interactions with the environment, including interactions with the player
*/ 

public class EnemyScript : MonoBehaviour {
	//used to set the health an enemy has
	[SerializeField] private int health = 1;

	//default x and z velocity
	[SerializeField] private float xVel = 0;
	[SerializeField] private float zVel = 0;

	//default patrolling distance
	[SerializeField] private float distance = 0;

	[SerializeField] private Transform playerT;
	//current distance traveled on patrolling path
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

	//used to save patroling angle
	private float angleBeforeEngage;

	// Use this for initialization
	void Start () {
		adjX = Mathf.Abs (xVel);
		adjZ = Mathf.Abs (zVel);
		angleBeforeEngage = this.transform.rotation.y;
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
	//Fires a bullet in the direction the enemy is facing
	void FireWeapon(){
		
	}

	void SearchForPlayer(){
		if (IsInLOS()) {
			angleBeforeEngage = this.transform.rotation.y;
			Vector3 lookAtPos = new Vector3(playerT.position.x, this.transform.position.y, playerT.position.z);
			transform.LookAt (lookAtPos);
			if (!firing) {
				//InvokeRepeating();
			}
			firing = true;

		} else if(firing){
			firing = false;
			this.transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleBeforeEngage,this.transform.rotation.z);
			CancelInvoke ();
		}
	}
	/* checks if the player is in the Field of view
	 * much of the code in IsInLOS and IsInFOV was obtained from http://unity.grogansoft.com/enemies-that-can-see/
	*/
	private bool IsInLOS(){
		float distanceToPlayer = Vector3.Distance(transform.position, playerT.position);
		RaycastHit[] rayHits = Physics.RaycastAll(transform.position, playerT.position - transform.position, distanceToPlayer);
		//Used for testing: Debug.DrawRay(transform.position, playerT.position - transform.position, Color.blue);
		foreach (RaycastHit hit in rayHits)
		{           
			if (hit.transform.tag != "Player")
			{
				return false;
			}
		}
		//player is reachable if no tags were hit
		return IsInFOV();
	}

	private bool IsInFOV(){
		Vector3 directionToPlayer = playerT.position - transform.position; // represents the direction from the enemy to the player    
		Debug.DrawLine(transform.position, playerT.position, Color.magenta); // a line drawn in the Scene window equivalent to directionToPlayer

		Vector3 lineOfSight = this.transform.forward; // the centre of the enemy's field of view, the direction of looking directly ahead
		Debug.DrawLine(transform.position, this.transform.forward, Color.yellow); // a line drawn in the Scene window equivalent to the enemy's field of view centre

		// calculate the angle formed between the player's position and the centre of the enemy's line of sight
		float angle = Vector3.Angle(directionToPlayer, lineOfSight);

		// if the player is within 65 degrees (either direction) of the enemy's centre of vision (i.e. within a 130 degree cone whose centre is directly ahead of the enemy) return true
		if (angle < 65)
			return true;
		else
			return false;
	}
}
