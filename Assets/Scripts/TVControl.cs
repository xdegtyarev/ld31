using UnityEngine;
using System.Collections;

public class TVControl : MonoBehaviour {
	[SerializeField] UnityEngine.UI.Text text;
	[SerializeField] Renderer renderer;
	[SerializeField] Animator doorAnimator;
	[SerializeField] AudioSource firstT;
	[SerializeField] AudioSource mainT;
	[SerializeField] AudioSource door;
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

	void OnTriggerStay(Collider other){
		if(other is CharacterController){
			if(Input.GetAxis("Action")>0f){
				TurnOffTV();
			}
		}
	}

	void TurnOffTV(){
		Destroy(firstT.gameObject);
		mainT.gameObject.SetActive(true);
		renderer.materials[0].color = Color.black;
		door.Play();
		doorAnimator.SetTrigger("OpenDoor");
		text.gameObject.SetActive(false);
		Destroy(this);
	}
}
