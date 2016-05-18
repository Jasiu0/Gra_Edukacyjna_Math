using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
    public Texture bacgroundTexture;
    private int pmenu = 0;
    public static int lv = 1;
    public static string username = "Player";
    void OnGUI()
    { 
        // Display background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),bacgroundTexture);

        if (pmenu == 0) {
            // Display buttons
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * .1f), "New Game")) {
                pmenu = 1;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.45f, Screen.width * 0.5f, Screen.height * .1f), "Options")) {
                pmenu = 2;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.6f, Screen.width * 0.5f, Screen.height * .1f), "Best Scores")) {
                pmenu = 3;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.75f, Screen.width * 0.5f, Screen.height * .1f), "Quit")) {
                Application.Quit();
            }
        } else if (pmenu == 1) {
            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * .1f), "Level 1")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 1;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.45f, Screen.width * 0.3f, Screen.height * .1f), "Level 2")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 2;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * .1f), "Level 3")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 3;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * .1f), "Level 4")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 4;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * .1f), "Level 5")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 5;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.45f, Screen.width * 0.3f, Screen.height * .1f), "Level 6")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 6;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * .1f), "Level 7")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 7;
                Application.LoadLevel("Mapa");
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * .1f), "Level 8")) {
                PlayerHealth.Health = 5;
                pmenu = 0;
                lv = 8;
                Application.LoadLevel("Mapa");
            }
        } else if (pmenu == 2) {
            GUI.Label(new Rect(Screen.width * 0.25f, Screen.height * 0.1f, Screen.width * 0.25f, Screen.height * 0.125f), "Player name: ");
            username = GUI.TextField(new Rect(Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 0.25f, Screen.height * 0.125f), username);

            if (GUI.Button(new Rect(0, 0, Screen.width * 0.15f, Screen.height * 0.08f), "Back")) {
                pmenu = 0;
            }
        } else if (pmenu == 3) {
            int scoresToShow = 10;
            DbHelper dbHelper = new DbHelper();
            List<ScoreModel> scores = dbHelper.GetScores(true, scoresToShow);

            GUIStyle headerStyle = new GUIStyle();
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.white;

            GUI.Box(new Rect(Screen.width * 0.25f, 0, Screen.width * 0.25f, Screen.height * 0.09f), "Player", headerStyle);
            GUI.Box(new Rect(Screen.width * 0.5f, 0, Screen.width * 0.25f, Screen.height * 0.09f), "Score", headerStyle);
            for (int i = 0; i < scoresToShow; i++) {
                GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * (i + 1) * 0.09f, Screen.width * 0.25f, Screen.height * 0.09f), scores[i].Name);
                GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * (i + 1) * 0.09f, Screen.width * 0.25f, Screen.height * 0.09f), scores[i].Score.ToString());
            }

            if (GUI.Button(new Rect(0, 0, Screen.width * 0.15f, Screen.height * 0.08f), "Back")) {
                pmenu = 0;
            }
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
