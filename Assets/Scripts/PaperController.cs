using UnityEngine;
using System.Collections;

public class PaperController : MonoBehaviour {
	public string paperType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void is_attacked(string from_type) {
		if ( from_type == paperType ) {
			Debug.Log(this.name + " est détruit par une attaque " + from_type + " !");
			this.SendMessageUpwards("aPaperIsDestroyed");
			Destroy(gameObject);
		} else {
			Debug.Log("Wrong Attack Type !");
		}
	}
}
