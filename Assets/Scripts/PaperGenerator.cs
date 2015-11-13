using UnityEngine;
using System.Collections;

public class PaperGenerator : MonoBehaviour {
	[System.Serializable]
	public class Paper {
		public GameObject prefab;
		public string type;
	}
	private const int CYCLE = 5; //Nombre de papiers à détuire à chaque palier de difficulté
	private const int MAXPAPER = 10; //Nombre de papiers maximum sur scène
	
	private int creationRateMin = 300; //Temps de création minimal entre deux papiers initialement (5s)
	private int creationRateMax = 420; //Temps de création maximal entre deux papiers initialement (7s)
	private int creationRate; //Temps effectif entre l'apparition de deux papiers
	private int creationCounter=0;
	
	public Paper[] paperTab = new Paper[3]; //Regroupera nos trois prefab papiers associés à leur type
	public GameObject sol; //Pour connaitre les limites de création de l'objet
	
	
	private int paperDestroyedCounter = 0; //Cycle comptant le nombre de papiers détruits. A chaque cycle, on réinitialise le compteur et on dimiue le range du creationRate
	private int paperCounter = 0;
	
	// Gestion des limites de spawn du papier
	private Bounds solBounds; // limite du sol
	private Vector3 paperSize; // taille du prefab papier sur X Y Z
	
	// Use this for initialization
	void Start () {
		paperSize = paperTab [0].prefab.GetComponent<Renderer> ().bounds.size; //Initialisation de la taille des papiers
		solBounds = sol.GetComponent<Renderer>().bounds; //Initialisation de la dimension du sol
		creationRate = Random.Range (creationRateMin, creationRateMax); //Initialisation du temps avant la première apparition de papier.
	}
	
	// Update is called once per frame
	void Update () {
		// SI on a pas atteint le nombre maximum de papiers sur la scène
		if (paperCounter < MAXPAPER) {
			//S'il est temps de créer un nouveau papier
			if (creationCounter == creationRate) {
				SpawnPaper ();
				creationCounter = 0;
				MajCreationRate ();
			} else {
				creationCounter++; //Sinon on incrémente le compteur de frames
			}
		}
	}
	
	void MajCreationRate() {
		//Si on atteint la fin d'un cycle/niveau, on augmente la fréquence d'apparition des papiers, si elle n'a pas atteint son maximum
		if (paperDestroyedCounter == CYCLE && creationRateMin > 60) { 
			creationRateMin -= 60;
			creationRateMax -= 60;
		}
		creationRate = Random.Range (creationRateMin, creationRateMax); //Maj du creationRate
	}
	
	public void SpawnPaper() {
		//Initialisation des coordonnées du nouveau papier
		float x = Random.Range(solBounds.min.x + paperSize.x/2, solBounds.max.x - paperSize.x/2);
		float y = solBounds.max.y + paperSize.y/2 + Random.Range(0, 10);
		float z = Random.Range(solBounds.min.z + paperSize.x/2, solBounds.max.z - paperSize.x/2);
		
		int type = Random.Range (0,3); //On détermine le type
		CreatePaper(new Vector3 (x, y, z), type);
	}
	
	void CreatePaper(Vector3 coordonate, int type) {
		GameObject newPaper = (GameObject) Instantiate(paperTab[type].prefab, coordonate, Quaternion.identity); //On crée le nouveau papier
		newPaper.GetComponent<PaperController>().paperType = paperTab[type].type; //On initialise le type du papier
		newPaper.transform.parent = this.transform; //On le lit à la PaperFactory
		
		paperCounter ++; //On augmente le nombre de papiers sur la scène
		Debug.Log("Creation d'un papier " + newPaper.name + " en (" + coordonate.x + ", " + coordonate.y + " , " + coordonate.z + ") de type " + paperTab[type].type );
	}
	
	public void APaperIsDestroyed () {
		paperDestroyedCounter++;
		paperCounter--;
	}
}
