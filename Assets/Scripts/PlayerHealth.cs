using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static int Health = 5;
    GameObject[] gameObjects;
    public Texture bacgroundTexture;
    private bool recordSaved = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.Health == 0)
        {
            Time.timeScale = 0.0f;
            if (!recordSaved)
            {
                CsvLogger.LogEvent("Game over with score: " + Oncollision.score);

                DbHelper dbHelper = new DbHelper();
                dbHelper.AddScoreRecord(MainMenu.username, Oncollision.score);
                Oncollision.score = 0;
                recordSaved = true;
            }

        }

    }

    void DestroyAllObjects(string tag)
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);
        /*   if (tag == "zombie")
           {
               PlayerHealth.Health += 5 + gameObjects.Length;
           }      */
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }

    }
    private GUIStyle guiStyle = new GUIStyle();
    private GUIStyle guiStyle_button;
    void OnGUI()
    {


        guiStyle_button = new GUIStyle(GUI.skin.button);
        guiStyle_button.normal.textColor = Color.white;
        guiStyle_button.fontSize = 30;  //must be int, obviously...
        guiStyle_button.alignment = TextAnchor.MiddleCenter;


        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.depth = 0;
        if (Options_click.Option == 0 && Oncollision.gameInProgress == true) 
        {
            GUI.Label(new Rect(Screen.width * 0.88f, Screen.height * 0.778f, 100, 20), Health.ToString(), guiStyle);
            if (Health == 0)
            {
                Affint.Stop();

                guiStyle.fontSize = 50;
                guiStyle.normal.textColor = Color.white;
                guiStyle.fontStyle = FontStyle.Italic;
                GUI.depth = 0;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bacgroundTexture);
                GUI.Label(new Rect(Screen.width * 0.28f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Game Over", guiStyle);

                if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.28f, Screen.width * 0.5f, Screen.height * .1f), "Restart", guiStyle_button))
                {
                    
                    Time.timeScale = 1;
                    DestroyAllObjects("zombie");
                    DestroyAllObjects("wybuch");
                    DestroyAllObjects("bomba");
                    PlayerHealth.Health = 5;
                    Affint.Start();
                }
                if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.42f, Screen.width * 0.5f, Screen.height * .1f), "Main Menu", guiStyle_button))
                {
                    Time.timeScale = 1;
                    DestroyAllObjects("zombie");
                    DestroyAllObjects("wybuch");
                    DestroyAllObjects("bomba");
                    Application.LoadLevel("MainMenu");
                }
                if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.55f, Screen.width * 0.5f, Screen.height * .1f), "Quit Game", guiStyle_button))
                {
                    Application.Quit();
                }

            }
        }
    }
}
