using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private int health = 50;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		// This determines whether or not the enemy was hit by a bullet
		if (collidedWith.tag == "Bullet") {
		}
	}
}
