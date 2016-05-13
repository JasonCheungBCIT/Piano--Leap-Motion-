using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PianoKey : MonoBehaviour {

	private float minimumDown = 0.5f;
	private Vector3 originalPosition;
	private Transform lowestBone;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// Check if key is being touched
		if (lowestBone != null) {
			float lowestPosition = lowestBone.position.y;

			// Lower bound 
			if (lowestPosition < (originalPosition.y - transform.localScale.y))
				lowestPosition = originalPosition.y - transform.localScale.y;
			
			// Upper bound 
			else if (lowestPosition > originalPosition.y)
				lowestPosition = originalPosition.y;

			// Set the position 
			transform.position = new Vector3(
				transform.position.x,
				lowestPosition,
				transform.position.z
			);
		}

	}
		
	/* Collision style - Allows volume control but much more laggy 
	void OnCollisionEnter(Collision collision) {
		if (!collision.transform.CompareTag ("Hand"))
			return;

		Debug.Log ("Collision enter");

		if (collided.Count == 0) {
			GetComponent<AudioSource>().volume = collision.relativeVelocity.magnitude / 25;	// range 0 to 10 
			GetComponent<AudioSource> ().Play ();	// Play audio if nothing was touching this key yet 
		}
		collided.Add (collision.transform);
	}
		
	void OnCollisionStay(Collision collision) {
		if (!collision.transform.CompareTag ("Hand"))
			return;
	}
	
	void OnCollisionExit(Collision collision) {
		if (!collision.transform.CompareTag ("Hand"))
			return;
		
		Debug.Log ("Collision detected");

		collided.Remove (collision.transform);
		if (collided.Count == 0) {
			transform.position = new Vector3 (
				originalPosition.x,
				originalPosition.y,
				originalPosition.z
			);
		}
	}
	*/

	/* Trigger style - supports volume control */
	void OnTriggerEnter(Collider other) {
		if (!other.transform.CompareTag ("Hand"))
			return;

		Debug.Log ("Trigger enter");

		// Nothing is touching the key yet 
		if (lowestBone == null) {
			GetComponent<AudioSource>().volume = other.GetComponent<Rigidbody>().velocity.magnitude / 25;	// range 0 to 1 
			GetComponent<AudioSource> ().Play ();	
			lowestBone = transform;
		}
	}


	void OnTriggerStay(Collider other) {
		if (!other.transform.CompareTag ("Hand"))
			return;

		// Debug.Log ("Trigger stay"); // causes lag

		// Update the lowest bone  
		if (transform.position.y < lowestBone.position.y)
			lowestBone = transform;
	}

	void OnTriggerExit(Collider other) {
		if (!other.transform.CompareTag ("Hand"))
			return;
		
		Debug.Log ("Trigger exit");

		// Remove the finger (if it's the last one touching)
		if (lowestBone == other.transform)
			lowestBone = null;
	}

}
