using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

	public GameObject particleSystem;

	IEnumerator OnTriggerEnter(Collider enemy){
		if (enemy.tag == "enemyAim") {

			var script = enemy.transform.root.GetComponent<HealthBarScript> ();
			if (script.cur_Health != 0) {
				script.cur_Health -= 30;
				script.SetHealthBar (script.cur_Health, script.max_Health);
			}
			GameObject ps = Instantiate (particleSystem, this.transform.position, this.transform.rotation);

			Destroy (this.gameObject);
		} else {
			yield return new WaitForSeconds (2f);
			Destroy (this.gameObject);
		}
	}
}
