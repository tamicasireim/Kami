using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public void LoadGame()
	{
		// Charge la scène principale
		Application.LoadLevel("scene");
	}

	public void LoadSynopsis()
	{
		// Charge le synopsis
		Application.LoadLevel ("synopsis");
	}

	public void LoadCredits()
	{
		// Charge les crédits
		Application.LoadLevel ("credits");
	}

	public void Quit()
	{
		// Ferme l'application
		Application.Quit();
	}
}
