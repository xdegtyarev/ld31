using UnityEngine;
using System.Collections;

public class FPSInputController : MonoBehaviour {
	[SerializeField] GameObject cam;
	[SerializeField] float sensivity;
	[SerializeField] CharacterController characterController;
	[SerializeField] float speed;
	[SerializeField] Animator animator;
	[SerializeField] GameObject flashlight;
	[SerializeField] AudioSource src;
	[SerializeField] AudioClip[] foot;
	[SerializeField] AudioSource flsfx;
	Transform cachedTransform;
	float prevToggle;

	// Use this for initialization
	void Awake(){
		cachedTransform = transform;
	}

	void Start () {
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
	}

	void LateUpdate(){
		LookInputUpdate();
		MoveInputUpdate();
		if(Time.timeSinceLevelLoad-prevToggle>1f){
			if(Input.GetAxis("Fire1")>0f){
				ToggleFlashLight();
			}
		}
	}

	void LookInputUpdate(){
		cam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(cam.transform.localRotation.eulerAngles.x - Input.GetAxis("Mouse Y")*sensivity,-360f, 360f),0f,0f);
		cachedTransform.localRotation = Quaternion.Euler(0f,cachedTransform.localRotation.eulerAngles.y+Input.GetAxis("Mouse X")*sensivity,0f);
	}

	void MoveInputUpdate(){
		var moveVector = new Vector3(Input.GetAxis("Horizontal")*speed,0f,Input.GetAxis("Vertical")*speed);
		if(moveVector.magnitude>0f){
			animator.SetBool("isWalking", true);	
			if(!src.isPlaying){
				src.clip = foot[Random.Range(0, foot.Length)];
				src.Play();
			}
		}else{
			animator.SetBool("isWalking", false);
			src.Stop();
		}

		moveVector -= Vector3.up * 9.8f;
		
		characterController.Move(cachedTransform.localToWorldMatrix*moveVector * Time.deltaTime);

	}

	void ToggleFlashLight(){
		prevToggle = Time.timeSinceLevelLoad;
		flashlight.SetActive(!flashlight.activeSelf);
		flsfx.Play();
	}
	
}