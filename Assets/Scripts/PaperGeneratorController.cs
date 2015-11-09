using UnityEngine;
using System.Collections;




public class PaperGeneratorController : MonoBehaviour {
	[System.Serializable]
	public class Paper {
		public GameObject prefab;
		public string type;
	}
	
	public Paper[] paper_tab = new Paper[3];
	//public GameObject prefabPaper; // le préfab papier
	public const int paperMax = 10; // Nombre max de papier généré
	public GameObject sol;


	private int paperCounter = 0; // nombre de papier généré
	private int paperNamingCounter = 1;

	// Gestion des limites de spawn du papier
	private Bounds solBounds; // limite du sol
	private Vector3 paperSize; // taille du prefab papier sur X Y Z
	
	// Use this for initialization
	void Start () {
		// initialisation des variables de la taille sol et taille des papiers.
		solBounds = sol.GetComponent<Renderer>().bounds;
		//paperSize = prefabPaper.GetComponent<Renderer>().bounds.size;

		// lance la fonction toutes les 5 secondes, à partir de la 5ème seconde
		InvokeRepeating("spawn_paper", 3, 3);

	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public GameObject spawn_paper() {
		if (paperCounter < paperMax) {
			float x = Random.Range(solBounds.min.x, solBounds.max.x);
			float y = solBounds.max.y + paperSize.y/2 + Random.Range(0, 10);
			float z = Random.Range(solBounds.min.z, solBounds.max.z);

			//on détermine le type
			int type = Random.Range (0,3);

			return create_paper(new Vector3 (x, y, z), type);
		} else {
			return null;
		}
	}

	GameObject create_paper(Vector3 coordonate, int type) {
		GameObject new_paper = (GameObject) Instantiate(paper_tab[type].prefab, coordonate, Quaternion.identity);

		// le nouvel objet est l'enfant du PaperGeneratorController
		new_paper.transform.parent = this.transform;

		// on renomme le papier et on incrémente le paperCounter
		paperCounter ++;
		new_paper.name = "Paper " + paperNamingCounter ++;

		//MAJ du type
		new_paper.GetComponent<PaperController>().paperType = paper_tab[type].type;

		Debug.Log("Creation d'un papier " + new_paper.name + " en (" + coordonate.x + ", " + coordonate.y + " , " + coordonate.z + ") de type " + paper_tab[type].type );

		return new_paper;
	}

	public void aPaperIsDestroyed () {
		paperCounter --;
	}
}
