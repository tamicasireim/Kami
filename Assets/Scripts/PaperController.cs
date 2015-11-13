using UnityEngine;
using System.Collections;

public class PaperController : MonoBehaviour {
	public string paperType;

	private bool destroy = false;
	private float audioVolume = 1.0f;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (destroy) {
			if (audioVolume > 0.01f) {
				fadeOut();
			} else {
				Destroy(gameObject);
			}
		}		
	}

	public void is_attacked(string from_type) {
		if ( from_type == paperType ) {
			Debug.Log(this.name + " est détruit par une attaque " + from_type + " !");
			this.SendMessageUpwards("aPaperIsDestroyed");

			// Destroy( GetComponent<Renderer>() );

			destroy = true;
			if (anim) {
				anim.SetBool("destroy",true);
			}
		} else {
			Debug.Log("Wrong Attack Type !");
		}
	}


	public void fadeOut() {
		// Debug.Log("Fading !" + audioVolume);
		if(audioVolume > 0.01f)
		{
			audioVolume -= 0.7f * Time.deltaTime;
			GetComponent<AudioSource>().volume = audioVolume;
		}
	}
}
