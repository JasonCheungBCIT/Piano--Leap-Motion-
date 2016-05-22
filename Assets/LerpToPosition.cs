using UnityEngine;
using System.Collections;

public class LerpToPosition : MonoBehaviour {

	public Vector3 startMarker, endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;

	void Start() {

	}

	void Update() {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
	}
		
	public void begin(Vector3 pos) {
		startMarker = transform.position;
		endMarker = pos;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, endMarker);
		this.enabled = true;
	}
}
