using UnityEngine;
using System.Collections;

public class ObjectFader : MonoBehaviour {

	public float fadeSpeed = 0.1f;

	private Renderer[] models;	// Holds all the renderers for this object and child objects.

	// Use this for initialization
	void Start () {

		models = GetComponentsInChildren<Renderer> ();
	
		foreach (Renderer r in models) {
			r.material.shader = Shader.Find("Transparent/Diffuse");	// Enabled alpha change 
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
		bool stillVisible = false;

		foreach (Renderer r in models) {
			// Fade object out 
			r.material.color = new Color (
				r.material.color.r,
				r.material.color.g,
				r.material.color.b,
				r.material.color.a - (fadeSpeed * Time.deltaTime)
			);

			if (r.material.color.a > 0)
				stillVisible = true;
		}

		if (!stillVisible) {
			GetComponent<ObjectFader> ().enabled = false;	// stop this script 
			gameObject.SetActive (false);					// no need for this object to be active
		}
	}
}
