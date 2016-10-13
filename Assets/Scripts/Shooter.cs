using UnityEngine;
using System.Collections;

/** 
 * Copyright (C) 2016 - James Greenwell
 * 
 * This is the class responsible for the player weapon
 **/

public class Shooter : MonoBehaviour
{
	//access to the bullet prefab
	[SerializeField] GameObject bulletPrefab;

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			FireWeapon ();
        }
    }
		
	void FireWeapon(){
		GameObject bulletObject = Instantiate (bulletPrefab) as GameObject;
		Vector3 bulletStart = this.transform.position + this.transform.forward*1f;
		bulletObject.transform.position = bulletStart;
		bulletObject.GetComponent<BulletTrajectory>().SetTrajectory (this.transform.forward);
	}
}