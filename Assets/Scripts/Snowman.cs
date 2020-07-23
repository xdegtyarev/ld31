using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snowman : MonoBehaviour {
	[SerializeField] CharacterController charController;
	[SerializeField] Transform flashlight;
	[SerializeField] float minimumDistance;
	[SerializeField] AudioSource src;
	[SerializeField] AudioClip[] clips;
	[SerializeField] Animator camAnimator;
	[SerializeField] NoiseAndGrain fx;
	[SerializeField] GrayscaleEffect gsFx;
	[SerializeField] SnowmanSpawnSettings[] settings;
	Transform[] snowmanSpawnPoints;
	[SerializeField] Transform spawnPointsParent;
	[SerializeField] MusicBoxCollectorManager collector;
	SnowmanSpawnSettings currentSettings;

	float timeSinceLastSpawn;
	float deathProgress;

	[SerializeField] float timeToRelax;
	[SerializeField] float timeToLookTillDeath;
	[SerializeField] float hissRelaxTime;
	void Awake(){
		snowmanSpawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
	}

	void Hiss(){
		if(!camAnimator.GetBool("Noisy")){
			camAnimator.SetBool("Noisy",true);
		}
		timeSinceLastSpawn = 0f;
		deathProgress += Time.deltaTime/timeToLookTillDeath;
		if(deathProgress>1f){
			Application.LoadLevel(1);
		}
		if(!src.isPlaying){
			fx.intensityMultiplier = Mathf.Lerp(1f,20f,1f-(Vector3.Distance(transform.position, flashlight.position)/minimumDistance));
			src.clip = clips[Random.Range(0, clips.Length)];
			src.Play();
		}

		hissRelaxTime = 1f;

	}
	
	void Update () {
		currentSettings = settings[collector.GetCollectedCount()];
		
		if(timeSinceLastSpawn>currentSettings.respawnTime){
			if(!IsInCameraFrustum() && deathProgress<=0.01f){
				Respawn();
			}
		}else{
			timeSinceLastSpawn+=Time.deltaTime;
		}

		var MoveV = (Camera.mainCamera.transform.position-transform.position).normalized;
		MoveV = new Vector3(MoveV.x,-9.8f,MoveV.z);
		charController.Move(MoveV*Time.deltaTime);

		if(Vector3.Distance(transform.position, flashlight.position)<3f){
			deathProgress = 10f;
		}

		if(Vector3.Distance(transform.position, flashlight.position)<minimumDistance && IsInCameraFrustum()){
			Hiss();
		}else{
			if(hissRelaxTime>0f){
				hissRelaxTime-=Time.deltaTime;
			}else{
				if(deathProgress>0f){
					deathProgress-=(Time.deltaTime/timeToRelax);
					if(deathProgress<0f){
						deathProgress = 0f;
					}
				}
				fx.intensityMultiplier = 2f;
				camAnimator.SetBool("Noisy",false);
				src.Stop();
			}
		}
		gsFx.rampOffset = -deathProgress;

	}

	bool IsInCameraFrustum(){
		Vector3 pos = Camera.mainCamera.WorldToViewportPoint(transform.position);
		// Debug.Log(pos);
		if(pos.z>0f && pos.x>0.1 && pos.x<0.8 && pos.y>0.2 && pos.y<0.8){
			return true;
			//return Physics.Raycast(transform.position,Camera.mainCamera.transform.position-transform.position);
		}
			return false;

	}

	void Respawn(){
		List<Transform> spawnPoints = new List<Transform>();
		float distance;
		foreach(var o in snowmanSpawnPoints){
			distance = Vector3.Distance(o.position, Camera.mainCamera.transform.position);
			if(distance<currentSettings.maxSpawnRadius && distance>currentSettings.minSpawnRadius){
				spawnPoints.Add(o);
			}
		}
		if(spawnPoints.Count>0){
			transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
			Debug.Log("Respawned to" + transform.position);
		}
		timeSinceLastSpawn = 0f;
	}	
}
