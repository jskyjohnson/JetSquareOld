using UnityEngine;
using System.Collections;

public class DeathBlock : MonoBehaviour {
	float a = 0F;
	public Color currentColor;
	// Use this for initialization
	void Start () {
		currentColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
	}
	// Update is called once per frame
	void Update () {
		if (a < 1F) {
			a += 0.1F;
			currentColor.a = a;
			this.gameObject.GetComponent<SpriteRenderer> ().color = currentColor;
		}
	}
}
