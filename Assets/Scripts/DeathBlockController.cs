using UnityEngine;
using System.Collections;

public class DeathBlockController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator decay() {
		yield return new WaitForSeconds(4.0f);
		Destroy (this.gameObject);
	}
}

