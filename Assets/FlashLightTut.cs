using UnityEngine;
using System.Collections;

public class FlashLightTut : MonoBehaviour {
	[SerializeField] UnityEngine.UI.Text text;

	void OnTriggerEnter(Collider other){
		if(other is CharacterController){
			text.gameObject.SetActive(true);
		}
	}


	void OnTriggerExit(Collider other){
		if(other is CharacterController){
			text.gameObject.SetActive(false);
		}
	}
}
