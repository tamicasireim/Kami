using UnityEngine;
using System.Collections;

public class InteractionWithPaper : MonoBehaviour {
	public const int RAYCASTLENGTH = 10000;	// Longueur du rayon issu de la caméra


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Le raycast attache un objet cliqué
		RaycastHit hitInfo;
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
		bool rayCasted = Physics.Raycast (ray, out hitInfo, RAYCASTLENGTH);
		
		if (rayCasted) 
		{
			rayCasted = hitInfo.transform.CompareTag("Paper");
		}

		// rayCasted est true si un objet possédant le tag Paper est détécté

		if (rayCasted) {
			GameObject paper = hitInfo.transform.gameObject;
			if (Input.GetKeyDown("a")) {
				//Debug.Log ("Je ne suis pas fou");
				paper.GetComponent<PaperController>().is_attacked("Cutable");
			}
			if (Input.GetKeyDown("z")) {
				paper.GetComponent<PaperController>().is_attacked("Crumplable");
			}
			if (Input.GetKeyDown("e")) {
				paper.GetComponent<PaperController>().is_attacked("Burnable");
			}
		}
	}

}
