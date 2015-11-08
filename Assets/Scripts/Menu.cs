using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void Quit()
	{
		// Ferme l'application
		Application.Quit();
	}
	
	public void LoadScene()
	{
		// Charge la scène principale
		Application.LoadLevel("scene");
	}
}
