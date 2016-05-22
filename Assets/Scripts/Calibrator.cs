using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Calibrator : MonoBehaviour {

	public GameObject leapMotionController;
    public TextFader  fadableText;
    // public Transform[] bones;
	public Transform leftHand, rightHand;
	public LerpToPosition piano;
	public float holdTime = 4.0f;

	private static string INSTRUC_1 = "place the leap motion controller on its side";
	private static string INSTRUC_2 = "rest your hands on the table";
	private static string INSTRUC_3 = "lift your hands, and let your table top become a piano";

    private static int FINGER_TIP_BUFFER = 10;  // The distance fingers can move to still be considered resting. 
    private Vector3[] lastTipPosition;          // [n] corresponds to the bones[n] 
    private bool handsResting = false;
	private bool checkingForRest = false;
	private float timeRested = 0.0f;
	private Vector3 lastLeftPos, lastRightPos;

    // Use this for initialization
	void Start () {
		StartCoroutine (PlayInitialAnimation());
	}
	
	// Update is called once per frame
	void Update () {

		// Note: checking directly doesn't work, as only the parent is enabled/disabled 
		if (checkingForRest && leftHand.parent.gameObject.activeSelf) {	
			// Calibrated! show final test and disable
			if (isHandsResting()) {
				Debug.Log ("Hands are resting!");
				StartCoroutine (PlayFinalAnimation ());
			}
		}
			
	}

    public IEnumerator PlayInitialAnimation()
    {
        yield return new WaitForSeconds(2f);							// short delay before animation starts 

		leapMotionController.GetComponent<Animator> ().enabled = true; 	// hack play animation
		yield return fadableText.fadeTextInAndOut (INSTRUC_1, 6);

		yield return fadableText.showText (INSTRUC_2);	

		checkingForRest = true;
	}

	public IEnumerator PlayFinalAnimation() {
		piano.begin (new Vector3 (
			0,
			lastLeftPos.y + lastRightPos.y / 2,	// average 
			lastLeftPos.z + lastRightPos.z / 2	// average
		));
		yield return fadableText.hideText ();
		yield return fadableText.fadeTextInAndOut (INSTRUC_3, 4f);
		this.gameObject.SetActive (false);
	}

	private bool isHandsResting() {
		if (!leftHand.gameObject.activeSelf || !rightHand.gameObject.activeSelf)
			return false;

		if (Vector3.Distance (lastLeftPos, leftHand.position) < FINGER_TIP_BUFFER
		    && Vector3.Distance (lastRightPos, rightHand.position) < FINGER_TIP_BUFFER
			&& Mathf.Abs(leftHand.position.y - rightHand.position.y) < FINGER_TIP_BUFFER) 
		{
			timeRested += Time.deltaTime;
			return (timeRested > holdTime);
		} else {
			lastLeftPos = leftHand.position;
			lastRightPos = rightHand.position;
			timeRested = 0;
			return false;
		}
	}

}
