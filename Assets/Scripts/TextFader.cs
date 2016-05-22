using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFader : MonoBehaviour {

	public float animationDuration = 2.0f;		  // in seconds

	private Text display;
	private const float DEFAULT_DURATION = 4.0f; // default time for text to hold during fade in and out 

	// Use this for initialization
	void Start () {
		display = GetComponent<Text> ();
	}
		
	public void setText(string text) {
		display.text = text;
	}

	/* Convenience methods */
	public IEnumerator showText() {
		StartCoroutine(opacityHelper(1));
		yield return new WaitForSeconds (animationDuration);
	}
	public IEnumerator showText(string text) {
		display.text = text;
		StartCoroutine(opacityHelper(1));
		yield return new WaitForSeconds (animationDuration);
	}

	public IEnumerator hideText() {
		StartCoroutine(opacityHelper(0));
		yield return new WaitForSeconds (animationDuration);
	}
		
	public IEnumerator fadeTextInAndOut() {
		StartCoroutine(fadeTextInAndOutHelper());
		yield return new WaitForSeconds (3 * animationDuration);	// in, hold, out (x3)
	}
	public IEnumerator fadeTextInAndOut(string text) {
		display.text = text;
		StartCoroutine(fadeTextInAndOutHelper());
		yield return new WaitForSeconds (3 * animationDuration);	// in, hold, out (x3)
	}
	public IEnumerator fadeTextInAndOut(string text, float duration) {
		display.text = text;
		StartCoroutine(fadeTextInAndOutHelper(duration));
		yield return new WaitForSeconds (2 * animationDuration + duration);	// in, hold, out (x3)
	}

	/* HELPERS */
	// Helps the text fade in and out 
	public IEnumerator fadeTextInAndOutHelper(float duration = DEFAULT_DURATION) {
		yield return StartCoroutine(opacityHelper(1));	// fade in
		yield return new WaitForSeconds(duration);		// show text for duration
		yield return StartCoroutine(opacityHelper(0));	// fade out 
	}
	
	// Helps animate an opacity change 
	public IEnumerator opacityHelper(float targetOpacity) {
		Color startColor = display.color;
		Color endColor   = new Color (display.color.r, display.color.g, display.color.b, targetOpacity);

		Debug.Log (startColor);
		Debug.Log (endColor);

		for (float t = 0.0f; t < animationDuration; t += Time.deltaTime) {
			display.color = Color.Lerp(startColor, endColor, t / animationDuration);
			yield return null;
		}

		display.color = endColor;
	}


}
