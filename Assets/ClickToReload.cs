using UnityEngine;
using System.Collections;

public class ClickToReload : MonoBehaviour {
	[SerializeField] UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		if(Time.timeSinceLevelLoad > 3f){
			if(!text.gameObject.activeSelf){
				text.gameObject.SetActive(true);
			}
			if(Input.anyKeyDown){
				Application.LoadLevel(0);
			}
		}
	}
}
