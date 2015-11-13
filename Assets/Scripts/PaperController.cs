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

	public void IsAttacked(string fromType) {
		if ( fromType == paperType ) {
			Debug.Log(this.name + " est détruit par une attaque " + fromType + " !");
			this.SendMessageUpwards("APaperIsDestroyed");

			// Destroy( GetComponent<Renderer>() ); // Pas nécessaire si on anime la disparition du papier

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
