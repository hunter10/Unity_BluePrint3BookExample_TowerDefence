using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	int state = 0;
	Transform[] waypoints = new Transform[9];
	Transform activeWayPoint;
	int currentWayPoint = 0;

	Transform myPos;
	float currentSpeed = 0.0f;
	float accel = 0.6f;
	float rotationDamping = 6.0f;

	GameObject wp1Obj;
	GameObject wp2Obj;
	GameObject wp3Obj;
	GameObject wp4Obj;
	GameObject wp5Obj;
	GameObject wp6Obj;
	GameObject wp7Obj;
	GameObject wp8Obj;
	GameObject wp9Obj;

	//public GameObject healthBarPrefab;
	//float currHealth;
	//float maxHealth;

	bool dead  = false;

	public GameObject deathParticle;

	GameObject gameObj;

	// Use this for initialization
	void Start () {
		state = 0;
		myPos = transform;
		wp1Obj = GameObject.Find ("waypoint_01");
		wp2Obj = GameObject.Find ("waypoint_02");
		wp3Obj = GameObject.Find ("waypoint_03");
		wp4Obj = GameObject.Find ("waypoint_04");
		wp5Obj = GameObject.Find ("waypoint_05");
		wp6Obj = GameObject.Find ("waypoint_06");
		wp7Obj = GameObject.Find ("waypoint_07");
		wp8Obj = GameObject.Find ("waypoint_08");
		wp9Obj = GameObject.Find ("waypoint_09");


		waypoints [0] = wp1Obj.transform;
		waypoints [1] = wp2Obj.transform;
		waypoints [2] = wp3Obj.transform;
		waypoints [3] = wp4Obj.transform;
		waypoints [4] = wp5Obj.transform;
		waypoints [5] = wp6Obj.transform;
		waypoints [6] = wp7Obj.transform;
		waypoints [7] = wp8Obj.transform;
		waypoints [8] = wp9Obj.transform;

		gameObj = GameObject.Find ("game");
	}
	
	// Update is called once per frame
	void Update () {
		//activeWayPoint = waypoints [currentWayPoint];
		//print (activeWayPoint.position);
		//Debug.DrawLine (myPos.position, activeWayPoint.position, Color.red);

		if (state == 0) {
			if (currentWayPoint != 9) {
				walk ();
				activeWayPoint = waypoints [currentWayPoint];
			} else {
				Game script1 = gameObj.transform.gameObject.GetComponent<Game> ();
				if (script1.baseHealth != 0) {
					script1.baseHealth -= 10;
					script1.enemiesLeft -= 1;
					print ("script1.enemiesLeft:" + script1.enemiesLeft);
				}

				enemySacrafice ();
				//Destroy (this.gameObject);
			}
		}

		Debug.DrawLine (myPos.position, activeWayPoint.position, Color.red);

		var script = GetComponent<HealthBarScript> ();
		float healthPercent = script.cur_Health / script.max_Health;
		if (healthPercent < 0) {
			healthPercent = 0;
		}

		if (dead != true) {
			if (script.cur_Health <= 0) {
				Game script2 = gameObj.transform.gameObject.GetComponent<Game> ();
				script2.playersWood += 10;
				script2.playerScore += 1000;
				script2.enemiesLeft -= 1;
				StartCoroutine(playDeath ());
				dead = true;
			}
		}
	}

	void enemySacrafice()
	{
		GameObject deathSmoke = Instantiate (deathParticle, this.transform.position, this.transform.rotation);
		Destroy (this.gameObject);
	}

	IEnumerator playDeath()
	{
		state = 1;
		Animation animation2 = gameObject.GetComponentInChildren<Animation> ();
		animation2.GetComponent<Animation> ().Play ("death");
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}

	void walk(){
		Quaternion rotation = Quaternion.LookRotation (waypoints [currentWayPoint].position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDamping);
		Vector3 waypointDirection = waypoints [currentWayPoint].position - transform.position;
		float speedFactor = Vector3.Dot (waypointDirection.normalized, transform.forward);
		float speed = accel * speedFactor;
		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "waypoint") {
			currentWayPoint++;
			//print ("currentWayPoint:" + currentWayPoint);
		}
	}

}
