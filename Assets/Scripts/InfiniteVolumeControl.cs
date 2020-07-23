using UnityEngine;
using System.Collections;

public class InfiniteVolumeControl : MonoBehaviour {
	Volume[] volume; //9 volumes
	[SerializeField] GameObject particleSystem;
	[SerializeField] float defaultSize;
	[SerializeField] Collider characterCollider;
	int currentVolume = 4;
	RaycastHit info;

	void Awake () {
		volume = new Volume[9];
		for(int i = 0;i<9;i++){
			volume[i] = (GameObject.Instantiate(particleSystem) as GameObject).GetComponent<Volume>();
			volume[i].control = this;
			volume[i].transform.parent = transform;
		}
		transform.position = characterCollider.transform.position;
		UpdatePosition();
	}

	public void OnEnteringVolume(int id){
		if(id == 4){
			return;
		}
		var newVolumeSet = new Volume[9];
		if(id == 1){
			for(int i = 0;i<9;i++){
				newVolumeSet[i] = volume[(i+6)%9];
			}
			volume = newVolumeSet;
		}
		if(id == 7){
			for(int i = 0;i<9;i++){
				newVolumeSet[i] = volume[(i+3)%9];
			}
			volume = newVolumeSet;
		}
		if(id == 5){
			for(int i = 0;i<3;i++){
				for(int j = 0;j<3;j++){
					newVolumeSet[i+j*3]=volume[((i+1)%3)+j*3];
				}
			}
			volume = newVolumeSet;
		}
		if(id == 3){
			for(int i = 0;i<3;i++){
				for(int j = 0;j<3;j++){
					newVolumeSet[i+j*3]=volume[((i+2)%3)+j*3];

				}
			}
			volume = newVolumeSet;
		}

		UpdatePosition();
	}	
	
	void UpdatePosition(){
		for(int i = 0;i<9;i++){
			if(volume[i]!=null){
				volume[i].SetId(i);
			}else{
				Debug.Log(i);
			}
		}

		volume[0].transform.position=volume[4].transform.position+new Vector3(-defaultSize, 0f, defaultSize);
		volume[1].transform.position=volume[4].transform.position+new Vector3(0f, 0f, defaultSize);
		volume[2].transform.position=volume[4].transform.position+new Vector3(defaultSize, 0f, defaultSize);
		volume[3].transform.position=volume[4].transform.position+new Vector3(-defaultSize, 0f, 0f);
		volume[5].transform.position=volume[4].transform.position+new Vector3(defaultSize, 0f, 0f);
		volume[6].transform.position=volume[4].transform.position+new Vector3(-defaultSize, 0f, -defaultSize);
		volume[7].transform.position=volume[4].transform.position+new Vector3(0f, 0f, -defaultSize);
		volume[8].transform.position=volume[4].transform.position+new Vector3(defaultSize, 0f, -defaultSize);
		for(int i = 0;i<9;i++){
			var p = volume[i].transform.position;
			volume[i].transform.position = new Vector3(p.x,characterCollider.transform.position.y + 50f,p.z);
		}
	}


	public void DisableSnow(){
		foreach(var o in volume){
			o.Disable();
		}
	}

	public void EnableSnow(){
		foreach(var o in volume){
			o.Enable();
		}
	}
}
