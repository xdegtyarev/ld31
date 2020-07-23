using UnityEngine;
using System.Collections;

public class MusicBoxCollectorManager : MonoBehaviour {
	[SerializeField] UnityEngine.UI.Text text;
	// Use this for initialization
	int collectedCount;
	int totalCount;
	void Start () {
		totalCount = GetComponentsInChildren<Transform>().Length-1;
	}

	public int GetCollectedCount(){
		return collectedCount;
	}
	
	public void OnCollected(MusicBox musicBox){
		collectedCount++;
		text.gameObject.SetActive(true);
		text.text = "Items collected: " + collectedCount + "/" + totalCount;
		StartCoroutine(HideText());
		if(collectedCount == totalCount){
			Application.LoadLevel(2);
		}
	}

	IEnumerator HideText(){
		yield return new WaitForSeconds(10f);
		text.gameObject.SetActive(false);		
	}
}
