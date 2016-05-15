using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Calibrator : MonoBehaviour {

	public GameObject leapMotionController;
    public TextFader  fadableText;
    public Transform[] bones;

	private static string INSTRUC_1 = "place the leap motion controller on its side";
	private static string INSTRUC_2 = "rest your hands on the table";
	private static string INSTRUC_3 = "lift your hands, and let your table top become a piano";

    private static int FINGER_TIP_BUFFER = 10;  // The distance fingers can move to still be considered resting. 
    private Vector3[] lastTipPosition;          // [n] corresponds to the bones[n] 
    private bool handsResting = false;
	private bool checkingForRest = false;

    // Use this for initialization
	void Start () {
		StartCoroutine (PlayInitialAnimation());
	}
	
	// Update is called once per frame
	void Update () {

		if (checkingForRest) {
			// checking for resting hands 
			handsResting = false;
			for (int i = 0; i < bones.Length; ++i) {
				if (Vector3.Distance (bones [i].position, lastTipPosition [i]) < FINGER_TIP_BUFFER) {
					// resting
					lastTipPosition [i] = bones [i].position;
				} else {
					// not resting 
					handsResting = false;
					break; 
				}
			}

			// Calibrated! show final test and disable
			if (handsResting) {
				fadableText.hideText ();
				fadableText.fadeTextInAndOut (INSTRUC_3, 4f);
				this.gameObject.SetActive (false);
			}
		}
	}

    public IEnumerator PlayInitialAnimation()
    {
		// TODO: darn, had to use the helpers directly
		Debug.Log("Waiting");
        yield return new WaitForSeconds(2f);							// short delay before animation starts 
		Debug.Log("moving controller and fading text");
		leapMotionController.GetComponent<Animator> ().enabled = true; 	// hack play animation
		fadableText.setText(INSTRUC_1);									
		yield return fadableText.fadeTextInAndOutHelper (6);

		Debug.Log("showing text");
		fadableText.showText (INSTRUC_2);	
		yield return null;

		checkingForRest = true;
		yield return null;
	}

}
