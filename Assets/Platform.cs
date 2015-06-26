using UnityEngine;
using System;

public class Platform : MonoBehaviour {
	public float r;
	public float g;
	public float b;
	float a = 0F;
	public Color currentColor;
	// Use this for initialization
	void Start () {
		r = UnityEngine.Random.Range (0.5F, 1F);
		g = UnityEngine.Random.Range (0.5F, 1F);
		b = UnityEngine.Random.Range (0.5F, 1F);
		currentColor = new Color(r, g, b);
		this.gameObject.GetComponent<SpriteRenderer> ().color = currentColor;
	}
	// Update is called once per frame
	void Update () {
		if (a < 1F) {
			a += 0.008F;
			currentColor.a = a;
			this.gameObject.GetComponent<SpriteRenderer> ().color = currentColor;
		}
	}
}
