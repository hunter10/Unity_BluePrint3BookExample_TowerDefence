using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	int noOfEnemies = 0;
	public GameObject tribeNormalPrefab;
	public GameObject tribeHeavyPrefab;
	Vector3 startPos;

	float spawnTime;
	float spawnTimeLeft;
	int spawnTimeSeconds = 2;
	int i = 0;
	int[] waveFormation = new int[]{0,0,0,1,1,0,1,0};

	public int playersWood = 200;
	int towerCost = 50;
	int totemCost = 100;
	string objectToPlaceNm;

	RaycastHit hit;
	Vector3 placementPos;
	public GameObject towerObj;
	public GameObject totemObj;
	int inc = 0;

	public GUIStyle towerBtn;
	public GUIStyle totemBtn;

	public Texture2D defenseWindow;
	public Texture2D scoreWindow;
	public Texture2D wood;
	public Texture2D health;

	public int baseHealth = 50;
	public int playerScore = 0;
	public int enemiesLeft;



	void OnGUI(){
		GUI.Label (new Rect (0, -3, 103, 317), defenseWindow);
		GUI.Label (new Rect (105, -3, 328, 64), scoreWindow);
		GUI.Label (new Rect (10, 23, 126, 98), wood);
		GUI.Label (new Rect (115, 0, 82, 68), health);

		GUI.Label (new Rect (85, 80, 100, 30), playersWood.ToString());
		GUI.Label (new Rect (160, 30, 100, 30), baseHealth.ToString());
		GUI.Label (new Rect (265, 33, 100, 30), playerScore.ToString());

		if (GUI.Button (new Rect (10, 135, 126, 98), "", towerBtn)) {
			if (playersWood >= towerCost) {
				objectToPlaceNm = "tower";
			}
		}

		if (GUI.Button (new Rect (10, 225, 126, 98), "", totemBtn)) {
			if (playersWood >= towerCost) {
				objectToPlaceNm = "totem";
			}
		}

		GUI.Label (new Rect (85, 190, 100, 30), "x50");
		GUI.Label (new Rect (85, 280, 100, 30), "x100");
	}

	void Awake(){
		spawnTime = Time.time;
	}

	// Use this for initialization
	void Start () {
		startPos = new Vector3 (-2.6f, 0, -3.1f);
		enemiesLeft = waveFormation.Length;
		print ("enemiesLeft:" + enemiesLeft);
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimeLeft = Time.time - spawnTime;
		if (spawnTimeLeft >= spawnTimeSeconds) {

			if (i != waveFormation.Length) {
				if (waveFormation [i] == 0) {
					Instantiate (tribeNormalPrefab, startPos, Quaternion.identity);
					spawnTime = Time.time;
					spawnTimeLeft = 0;
					i++;
				} else {
					Instantiate (tribeHeavyPrefab, startPos, Quaternion.identity);
					spawnTime = Time.time;
					spawnTimeLeft = 0;
					i++;
				}
			}
		}

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.collider.tag != "ground" &&
				   hit.collider.tag != "enemyAim" &&
				   hit.collider.tag != "temple") {
					if (objectToPlaceNm == "tower") {
						playersWood = playersWood - towerCost;
						placementPos = hit.transform.position;
						GameObject arrTwr = Instantiate (towerObj, placementPos, Quaternion.identity);
						arrTwr.name = inc.ToString ();
						Destroy (hit.collider.gameObject);
						objectToPlaceNm = "";
					}
					if (objectToPlaceNm == "totem") {
						playersWood = playersWood - totemCost;
						placementPos = hit.transform.position;
						GameObject totem = Instantiate (totemObj, placementPos, Quaternion.identity);
						totem.transform.Rotate (0, 180, 0);
						totem.name = inc.ToString ();
						Destroy (hit.collider.gameObject);
						objectToPlaceNm = "";
					}
				}
			}
		}

		if (enemiesLeft == 0) {
			if (baseHealth == 0) {
				print ("Game Over - You lose");
			} else {
				print ("Game Over - You Win");
			}
		} else {
			if (baseHealth == 0) {
				print ("Game Over - You lose");
			}
		}

	}
}
