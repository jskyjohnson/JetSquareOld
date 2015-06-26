using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public float xOffset = 0.0f;
	public float zOffset = 0.0f;
	private void Start()
	{
	}

	private void Update()
	{
		Vector3 cameraPosition = this.transform.position;
		cameraPosition.y = player.transform.position.y - 2.0f;
		this.transform.position = cameraPosition;
	}
}
