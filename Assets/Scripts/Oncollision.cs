using UnityEngine;
using System.Collections;

public class Oncollision : MonoBehaviour {
    // Use this for initialization
    public static int score = 0;
    public static int zombieKilled = 0;

    public static  bool gameInProgress = true;

    private GameObject[] gameObjects;
    private GameObject questionView;

    private bool recordSaved = false;

    private bool showText = false;
    private float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 5.0f;
    private bool show = false;
    public Texture bacgroundTexture;
    void Start () {
        questionView = GameObject.FindWithTag("questionView");
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        if(show)
             showText = true;
         
         if(executedTime != 0.0f)
         {
             if(currentTime - executedTime > timeToWait)
             {
                 executedTime = 0.0f;
                 showText = false;
                 show = false;
             }
         }

        if (!gameInProgress) {
            Time.timeScale = 0.0f;
            if (!recordSaved) {
                DbHelper dbHelper = new DbHelper();
                dbHelper.AddScoreRecord(MainMenu.username, score);
                score = 0;
                recordSaved = true;
            }
        }
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("KOLIZJA!");
        if (coll.gameObject.tag == "zombie")
        {
            CsvLogger.LogEvent("Zombie killed");
            Destroy(coll.gameObject);
          //  PlayerHealth.Health += 1;
            score += MainMenu.lv;

            zombieKilled++;
            if (zombieKilled > 14) {
                gameInProgress = ++MainMenu.lv < 9;
                if (gameInProgress) {
                    CsvLogger.LogEvent("Level " + MainMenu.lv + " started");
                } else {
                    CsvLogger.LogEvent("Game completed with score = " + score);
                }
                
                questionView.SendMessage("OnNewLevel", null);
                zombieKilled = 0;
                show = true;

                EscapeOfZombies();
                executedTime = Time.time;

                Affint.Stop();
                Affint.Start();
            }

        }

    }

  
    void DestroyAllObjects(string tag) {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (var i = 0; i < gameObjects.Length; i++) {
            Destroy(gameObjects[i]);
        }
    }

    void EscapeOfZombies()
    {    
        gameObjects = GameObject.FindGameObjectsWithTag("zombie");
       // PlayerHealth.Health += gameObjects.Length-1;
        DestroyAllObjects("zombie");
            
    }

    private GUIStyle guiStyle = new GUIStyle();
    private GUIStyle guiStyle_button;
    void OnGUI() {
        guiStyle_button = new GUIStyle(GUI.skin.button);
        guiStyle_button.normal.textColor = Color.white;
        guiStyle_button.fontSize = 30;
        guiStyle_button.alignment = TextAnchor.MiddleCenter;

        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontStyle = FontStyle.Italic;
        GUI.depth = 0;
        if (gameInProgress) {
            if (showText)
            {
                GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Level" + MainMenu.lv + "!", guiStyle);
            }
  

            return;
        }

        Affint.Stop();
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bacgroundTexture);
        GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Game Completed!", guiStyle);

        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.28f, Screen.width * 0.5f, Screen.height * .1f), "Restart", guiStyle_button))
        {
            gameInProgress = true;
            Time.timeScale = 1;
            DestroyAllObjects("zombie");
            DestroyAllObjects("wybuch");
            DestroyAllObjects("bomba");
            MainMenu.lv = 1;
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
