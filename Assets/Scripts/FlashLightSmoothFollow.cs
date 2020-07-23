using UnityEngine;
using System.Collections;

public class FlashLightSmoothFollow : MonoBehaviour {
	[SerializeField] Transform characterTransform;
	[SerializeField] GameObject spotLight;
	[SerializeField] Transform camera;
	float stepShakeTime = 1f;
	void Update(){
		// transform.position = characterTransform.position;
		if(stepShakeTime<0f){
			//iTween.ShakeRotation(spotLight, Vector3.up*2f, 1f);
			stepShakeTime = 1f;
		}else{
			stepShakeTime -= Time.deltaTime;
		}
	}
}
