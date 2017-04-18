using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {

	string ttag = "enemyAim";
	Transform target;

	Transform closestEnemy = null;
	float dist;

	float startTime;
	float shootTimeLeft;
	float shootTimeSeconds = 1.0f;
	public GameObject projPrefab;
	float initialSpeed = 3.0f;
	Vector3 targetDir;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("getClosestEnemy", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Debug.DrawLine (transform.position, target.position, Color.yellow);
			targetDir = target.position - transform.position;
			transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
			shootTimeLeft = Time.time - startTime;
			if (shootTimeLeft >= shootTimeSeconds) {
				Fire ();
				//print ("Fire!");
				startTime = Time.time;
				shootTimeLeft = 0;
			}
		}
	}

	void Fire()
	{
		GameObject instantiatedProjectile = Instantiate (projPrefab, transform.position, transform.rotation);
		//instantiatedProjectile.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (targetDir * initialSpeed);
	}

	void getClosestEnemy(){
		//print ("get closestEnemy");

		GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag (ttag);
		float closestDistSqr = Mathf.Infinity;
		Transform closestEnemy = null;

		foreach (var taggedEnemy in taggedEnemys) {
			var objectPos = taggedEnemy.transform.position;
			dist = (objectPos - transform.position).sqrMagnitude;

			if (dist < 3.0) {
				if (dist < closestDistSqr) {
					closestDistSqr = dist;
					closestEnemy = taggedEnemy.transform;
				}
			}
		}

		target = closestEnemy;
	}
}
