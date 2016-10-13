using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 * 
 * This class controls bullet trajectories
 **/

public class BulletTrajectory : MonoBehaviour {

	//controls bullet speed
	private float speed;
	//controls bullet trajectory direction
	private Vector3 t;

	// Use this for initialization
	void Start () {
		//t = new Vector3(0,0,0);
		speed = 1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 updatePos = this.transform.position + (t * speed);
		this.transform.position = updatePos;
	}
	public void SetTrajectory(Vector3 trajectoryDir){
		t = trajectoryDir;
	}

	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		// Handles wall hits
		if (collidedWith.tag == "Wall") {
			Destroy (gameObject);
		}
		// Bullets destroy other bullets
		if (collidedWith.tag == "Bullet") {
			Destroy (gameObject);
		}
	}
}
