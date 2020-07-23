using UnityEngine;
using System.Collections;

public class MusicBox : MonoBehaviour {
	[SerializeField] MusicBoxCollectorManager collector;
	[SerializeField] AudioSource src;
	[SerializeField] AudioClip clip;
	void OnTriggerEnter(Collider other){
		if(other is CharacterController){
			collector.OnCollected(this);
			src.clip = clip;
			src.Play();
			Destroy(gameObject);
		}
	}
}
