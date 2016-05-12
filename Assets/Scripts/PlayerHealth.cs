using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public static  int Health = 5;
    GameObject[] gameObjects;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerHealth.Health == 0)
        {
            Time.timeScale = 0.0f;
        }

	}

    void DestroyAllObjects(string tag)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (tag == "zombie")
        {
            PlayerHealth.Health += 5 + gameObjects.Length;
        }      
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }

    }
    private GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    { 
        
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.depth = 0;
        GUI.Label(new Rect(Screen.width-72,Screen.height-78, 100, 20), Health.ToString(),guiStyle);
        if (Health == 0)
        {
            guiStyle.fontSize = 50;
            guiStyle.normal.textColor = Color.white;
            guiStyle.fontStyle = FontStyle.Italic;
            GUI.depth = 0;
            GUI.Label(new Rect(Screen.width * 0.28f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Game Over", guiStyle);
            
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.28f, Screen.width * 0.5f, Screen.height * .1f), "Restart"))
             {
                Time.timeScale = 1;
                DestroyAllObjects("zombie");
                DestroyAllObjects("wybuch");
                DestroyAllObjects("bomba");
            }
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.42f, Screen.width * 0.5f, Screen.height * .1f), "Main Menu"))
            {
                Time.timeScale = 1;
                DestroyAllObjects("zombie");
                DestroyAllObjects("wybuch");
                DestroyAllObjects("bomba");
                PlayerHealth.Health = 5;
                Application.LoadLevel("MainMenu"); 
            }
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.55f, Screen.width * 0.5f, Screen.height * .1f), "Quit Game"))
            {
                Application.Quit();
            }
            
        }
    }
}
