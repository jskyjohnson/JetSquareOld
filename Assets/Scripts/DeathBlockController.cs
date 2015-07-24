using UnityEngine;
using System.Collections;

public class DeathBlockController : MonoBehaviour
{
	IEnumerator decay() {
		yield return new WaitForSeconds(4.0f);
		Destroy (this.gameObject);
	}
}

