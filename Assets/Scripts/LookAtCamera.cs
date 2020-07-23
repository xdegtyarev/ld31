using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		var euler = Quaternion.LookRotation((transform.position-Camera.mainCamera.transform.position).normalized).eulerAngles;
		transform.localRotation = Quaternion.Euler(0f,euler.y,0f);
	}
}
