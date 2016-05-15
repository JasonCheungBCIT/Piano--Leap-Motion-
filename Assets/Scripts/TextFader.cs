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

	public void showText() {
		StartCoroutine(opacityHelper(1));
	}
	public void showText(string text) {
		display.text = text;
		StartCoroutine(opacityHelper(1));
	}

	public void hideText() {
		StartCoroutine(opacityHelper(0));
	}
		
	public void fadeTextInAndOut() {
		StartCoroutine(fadeTextInAndOutHelper());
	}
	public void fadeTextInAndOut(string text) {
		display.text = text;
		StartCoroutine(fadeTextInAndOutHelper());
	}
	public void fadeTextInAndOut(string text, float duration) {
		display.text = text;
		StartCoroutine(fadeTextInAndOutHelper(duration));
	}

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
