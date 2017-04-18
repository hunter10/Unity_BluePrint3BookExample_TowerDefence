﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	Camera cameraToLookAt;

	// Use this for initialization
	void Start () {
		cameraToLookAt = Camera.main;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = cameraToLookAt.transform.position - transform.position;
		v.x = v.z = 0.0f;
		transform.LookAt (cameraToLookAt.transform.position - v);
	}
}
