using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
    public static int Option = 0;
    public Texture bacgroundTexture;
    GameObject[] gameObjects;
    int temp = 0;

	// Use this for initialization
	void Start () {
	
	}
	


    void OnMouseDown()
    {
        Option = 1;
        Time.timeScale = 0.0f;

    }

    void DestroyAllObjects(string tag)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (tag == "zombie")
        {
            PlayerHealth.Health += 5 + gameObjects.Length - temp;
        }
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        
    }
    private GUIStyle guiStyle_button;
    private GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
    {

        guiStyle_button = new GUIStyle(GUI.skin.button);
        guiStyle_button.normal.textColor = Color.white;
        guiStyle_button.fontSize = 30; 
        guiStyle_button.alignment = TextAnchor.MiddleCenter;

        if (Option == 1)
        {
            Affint.Pause();

            guiStyle.fontSize = 50;
            guiStyle.normal.textColor = Color.white;
            guiStyle.fontStyle = FontStyle.Italic;
            GUI.depth = 0;
            GUI.Label(new Rect(Screen.width * 0.37f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Options", guiStyle);

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.28f, Screen.width * 0.5f, Screen.height * .1f), "Resume", guiStyle_button))
            {
                Time.timeScale = 1;
                Option = 0;

                Affint.Start();
            }
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.42f, Screen.width * 0.5f, Screen.height * .1f), "Restart", guiStyle_button))
            {
                temp = PlayerHealth.Health;
                Time.timeScale = 1;
                DestroyAllObjects("zombie");
                DestroyAllObjects("wybuch");
                DestroyAllObjects("bomba");
                Option = 0;

                Affint.Stop();
                Affint.Start();
            }
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.55f, Screen.width * 0.5f, Screen.height * .1f), "Main Menu", guiStyle_button))
            {
                Time.timeScale = 1;
                DestroyAllObjects("zombie");
                DestroyAllObjects("wybuch");
                DestroyAllObjects("bomba");
                Option = 0;
                Application.LoadLevel("MainMenu");

                Affint.Stop();
            }
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.68f, Screen.width * 0.5f, Screen.height * .1f), "Quit Game", guiStyle_button))
            {
                Affint.Stop();
                Application.Quit();
            }
            
        }
    }
}
