using UnityEngine;
using System.Collections;

public class Oncollision : MonoBehaviour {
    // Use this for initialization
    public static int score = 0;
    public static int zombieKilled = 0;

    private bool gameInProgress = true;

    private GameObject[] gameObjects;
    private GameObject questionView;

    private bool recordSaved = false;
    void Start () {
        questionView = GameObject.FindWithTag("questionView");
    }
	
	// Update is called once per frame
	void Update () {
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
            Destroy(coll.gameObject);
            PlayerHealth.Health += 1;
            score += MainMenu.lv;

            zombieKilled++;
            if (zombieKilled > 5) {
                gameInProgress = ++MainMenu.lv < 9;
                questionView.SendMessage("OnNewLevel", null);
                zombieKilled = 0;               
            }

        }

    }

    void DestroyAllObjects(string tag) {
        gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (var i = 0; i < gameObjects.Length; i++) {
            Destroy(gameObjects[i]);
        }

    }

    private GUIStyle guiStyle = new GUIStyle();
    void OnGUI() {
        if (gameInProgress) {
            return;
        }

        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.depth = 0;
        GUI.Label(new Rect(Screen.width - 72, Screen.height - 78, 100, 20), PlayerHealth.Health.ToString(), guiStyle);

        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontStyle = FontStyle.Italic;
        GUI.depth = 0;
        GUI.Label(new Rect(Screen.width * 0.28f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Game Completed!", guiStyle);

        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.28f, Screen.width * 0.5f, Screen.height * .1f), "Restart")) {
            Time.timeScale = 1;
            DestroyAllObjects("zombie");
            DestroyAllObjects("wybuch");
            DestroyAllObjects("bomba");
        }
        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.42f, Screen.width * 0.5f, Screen.height * .1f), "Main Menu")) {
            Time.timeScale = 1;
            DestroyAllObjects("zombie");
            DestroyAllObjects("wybuch");
            DestroyAllObjects("bomba");
            Application.LoadLevel("MainMenu");
        }
        if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.55f, Screen.width * 0.5f, Screen.height * .1f), "Quit Game")) {
            Application.Quit();
        }


    }

}
