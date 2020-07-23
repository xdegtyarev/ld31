using UnityEngine;
using System.Collections;

public class Volume : MonoBehaviour {
	public InfiniteVolumeControl control;
	[SerializeField] ParticleSystem ps;
	int id;
	public void SetId(int i){
		id = i;
		if(id == 4){
			ps.emissionRate = 2000f;
		}else{
			ps.emissionRate = 500f;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other is CharacterController){
			control.OnEnteringVolume(id);
		}
	}

	public void Disable(){
		ps.enableEmission = false;
	}

	public void Enable(){
		ps.enableEmission = true;
	}


}
