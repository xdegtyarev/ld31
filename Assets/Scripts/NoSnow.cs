using UnityEngine;
using System.Collections;

public class NoSnow : MonoBehaviour {
	[SerializeField] InfiniteVolumeControl control;
	void OnTriggerEnter(Collider other){
		if(other is CharacterController){
			control.DisableSnow();
		}
	}

	void OnTriggerExit(Collider other){
		if(other is CharacterController){
			control.EnableSnow();
		}
	}
}
