using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PianoKey : MonoBehaviour {

	private float minimumDown = 0.5f;
	private List<Transform> collided;
	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		collided = new List<Transform> ();
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// Check if key is being touched
		if (collided.Count != 0) {

			// Find the lowest position
			float lowestPosition = collided[0].position.y;
			foreach (Transform t in collided) {
				if (t.position.y < transform.position.y) {
					lowestPosition = t.position.y;
				}
			}

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
		
	/* Collision style - Allows volume control but much more laggy */
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Collision enter");

		if (collided.Count == 0) {
			GetComponent<AudioSource>().volume = collision.relativeVelocity.magnitude / 25;	// range 0 to 10 
			GetComponent<AudioSource> ().Play ();	// Play audio if nothing was touching this key yet 
		}
		collided.Add (collision.transform);
	}
		
	void OnCollisionStay(Collision collision) {

	}
	
	void OnCollisionExit(Collision collision) {
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

	/* Trigger style - No volume control but less laggy 
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger enter");

		if (collided.Count == 0) 
			GetComponent<AudioSource> ().Play ();	// Play audio if nothing was touching this key yet 
		
		collided.Add (other.transform);
	}


	void OnTriggerStay(Collider other) {
		// Debug.Log ("Trigger stay"); // causes lag
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Trigger exit");

		collided.Remove (other.transform);
	}
	*/
}
