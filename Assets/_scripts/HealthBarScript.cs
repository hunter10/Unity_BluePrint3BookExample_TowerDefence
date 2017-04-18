using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

	public float max_Health = 100f;
	public float cur_Health = 0f;
	public GameObject healthBarPref;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		cur_Health = max_Health;
		//InvokeRepeating ("decreseHealth", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		//healthBarPref.transform.position = Camera.main.WorldToViewportPoint (transform.position);
		//healthBarPref.transform.position = Camera.main.WorldToScreenPoint (transform.position);
		//healthBarPref.transform.LookAt(Camera.main.transform);

		healthBarPref.transform.rotation = Quaternion.identity;
	}

	void decreseHealth(){
		cur_Health -= 2f;
		if (cur_Health <= 0) {
			cur_Health = 0;
			CancelInvoke ();
		}
		float calc_Health = cur_Health / max_Health;
		SetHealthBar (calc_Health);
	}

	public void SetHealthBar(float myHealth, float maxHealth){
		float calc_Health = myHealth / maxHealth;
		SetHealthBar (calc_Health);
	}

	public void SetHealthBar(float myHealth){
		healthBar.transform.localScale = new Vector3 (myHealth, 1f, 1f);
	}
}
