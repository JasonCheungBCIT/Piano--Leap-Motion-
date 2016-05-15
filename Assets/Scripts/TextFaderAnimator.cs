using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFaderAnimator : MonoBehaviour {

    private Text display;
    private Animator _animator;

	// Use this for initialization
	void Start () {
		display = GetComponent<Text> ();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText(string text) {
		display.text = text;
	}

    public void showText()
    {
        _animator.SetBool("TextVisible", true);
    }

    public void showText(string text)
    {
        display.text = text;
        _animator.SetBool("TextVisible", true);
    }

    public void hideText()
    {
        _animator.SetBool("TextVisible", false);
    }

    public void fadeTextInAndOut()
    {
        _animator.SetTrigger("FadeInOutTrigger");
    }

    public void fadeTextInAndOut(string text)
    {
        display.text = text;
        _animator.SetTrigger("FadeInOutTrigger");
    }

    public void fadeTextInAndOut(string text, float duration)
    {
		StartCoroutine(fadeTextInAndOutHelper(text, duration));
    }

	public IEnumerator fadeTextInAndOutHelper(string text, float duration) {
		_animator.SetBool("TextVisible", true);
		yield return new WaitForSeconds(duration);
		_animator.SetBool("TextVisible", false);
	}


}
