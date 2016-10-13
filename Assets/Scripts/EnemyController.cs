/** 
 * Copyright (C) 2016 - James Greenwell
 **/

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	//used to set the health an enemy has
	[SerializeField] private int health = 2;

	//determines the number of points the enemy is worth
	private int points = 50;

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

	//access to the bullet prefab
	[SerializeField] GameObject bulletPrefab;

	//controls the rate at which bullets are fired
	private float fireRate = .5f;

	// Used for initialization
	void Start () {
		adjX = Mathf.Abs (xVel);
		adjZ = Mathf.Abs (zVel);
		angleBeforeEngage = this.transform.eulerAngles.y;
	}

	// Responsible for enemy movement and interaction
	void FixedUpdate () {
		SearchForPlayer ();
		Patrol ();
	}

	void Patrol(){
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
			}  else {
				if (rotated < 180) {
					this.transform.Rotate (0, 2, 0);
					rotated += 2;

				}  else {
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
			Destroy (collidedWith);
			health = health - 1;
			if (health == 0) {
				FPSController controller = FindObjectOfType<FPSController>();
				controller.UpdateScore (points);
				Destroy (gameObject);
			}
		}
		Debug.Log ("hit");
	}
	//Fires a bullet in the direction the enemy is facing
	void FireWeapon(){
		GameObject bulletObject = Instantiate (bulletPrefab) as GameObject;
		Vector3 bulletStart = this.transform.position + this.transform.forward*1f;
		bulletStart.y = 1.8f;
		bulletObject.transform.position = bulletStart;
		bulletObject.GetComponent<BulletTrajectory>().SetTrajectory (this.transform.forward);
	}

	void SearchForPlayer(){
		if (IsInLOS()) {
			if (!firing) {
				angleBeforeEngage = this.transform.eulerAngles.y;
				Debug.Log (angleBeforeEngage);
				InvokeRepeating ("FireWeapon", .1f, fireRate);
				firing = true;
			}
			Vector3 lookAtPos = new Vector3(playerT.position.x, this.transform.position.y, playerT.position.z);
			transform.LookAt (lookAtPos);
		}  else if(firing){
			this.transform.eulerAngles = new Vector3(this.transform.rotation.x,angleBeforeEngage,this.transform.rotation.z);
			firing = false;
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
		Vector3 directionToPlayer = playerT.position - transform.position;  //Direction to player
		Debug.DrawLine(transform.position, playerT.position, Color.magenta); //draws a line to the player from the enemy

		Vector3 lineOfSight = this.transform.forward; // the center of the enemy's field of view
		Debug.DrawLine(transform.position, this.transform.forward, Color.yellow); // draws line to represent center of FOV

		// calculate the angle formed between the player's position and the centre of the enemy's line of sight
		float angle = Vector3.Angle(directionToPlayer, lineOfSight);

		// if the player is visible and within 65 degrees of the central FOV ray
		if (angle < 65) {
			return true;
		}  else {
			return false;
		}
	}
}
