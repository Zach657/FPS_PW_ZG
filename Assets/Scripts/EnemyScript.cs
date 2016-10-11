using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private int health = 1;
	private float xVel = .1f;
	private float zVel = .1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Responsible for enemy movement
	void FixedUpdate () {
		Vector3 updatePos = gameObject.transform.position;
		updatePos.x = updatePos.x + xVel;
		updatePos.y = updatePos.z + zVel;
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
}
