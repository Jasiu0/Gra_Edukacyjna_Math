using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public Texture bacgroundTexture;
    void OnGUI()
    { 
        // Display background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),bacgroundTexture);

        // Display buttons
        if (GUI.Button(new Rect(Screen.width * 0.25f , Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * .1f), "New Game"))
        {
            PlayerHealth.Health = 5;
            Application.LoadLevel("Mapa");
        }

        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.45f, Screen.width * 0.5f, Screen.height * .1f), "Options"))
        {

        }

        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.6f, Screen.width * 0.5f, Screen.height * .1f), "Best Scores"))
        {

        }

        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.75f, Screen.width * 0.5f, Screen.height * .1f), "Quit"))
        {
            Application.Quit();
        }

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
