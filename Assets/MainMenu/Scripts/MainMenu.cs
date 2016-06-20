using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public Texture bacgroundTexture;
    private int pmenu = 0;
    public static int lv = 1;
    public static string username = "Player";
    public Font font; //set it in the inspector
    private GUIStyle guiStyle;
    public static bool music = true;
    void OnGUI()
    {
        // Display background texture


        //in awake or start use these lines of code to set the size of the font
        guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.font = font;
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 30;  //must be int, obviously...
        guiStyle.alignment = TextAnchor.MiddleCenter;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bacgroundTexture);
        if (pmenu != 0)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width * 0.15f, Screen.height * 0.08f), "Back",guiStyle))
            {
                pmenu = 0;
            }
        }

        if (pmenu == 0)
        {
            // Display buttons
            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.3f, Screen.width * 0.5f, Screen.height * .1f), "New Game", guiStyle))
            {
                pmenu = 1;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.45f, Screen.width * 0.5f, Screen.height * .1f), "Options", guiStyle))
            {
                pmenu = 2;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.6f, Screen.width * 0.5f, Screen.height * .1f), "Best Scores", guiStyle))
            {
                pmenu = 3;
            }

            if (GUI.Button(new Rect(Screen.width * 0.25f, Screen.height * 0.75f, Screen.width * 0.5f, Screen.height * .1f), "Quit", guiStyle))
            {
                Application.Quit();
            }
        }
        else if (pmenu == 1)
        {
            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * .1f), "Level 1", guiStyle))
            {
                startGame(1);
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.45f, Screen.width * 0.3f, Screen.height * .1f), "Level 2", guiStyle))
            {
                startGame(2);
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * .1f), "Level 3", guiStyle))
            {
                startGame(3);
            }

            if (GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * .1f), "Level 4", guiStyle))
            {
                startGame(4);
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * .1f), "Level 5", guiStyle))
            {
                startGame(5);
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.45f, Screen.width * 0.3f, Screen.height * .1f), "Level 6", guiStyle))
            {
                startGame(6);
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.6f, Screen.width * 0.3f, Screen.height * .1f), "Level 7", guiStyle))
            {
                startGame(7);
            }

            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.75f, Screen.width * 0.3f, Screen.height * .1f), "Level 8", guiStyle))
            {
                startGame(8);
            }
        }
        else if (pmenu == 2)
        {
            GUI.Label(new Rect(Screen.width * 0.15f, Screen.height * 0.1f, Screen.width * 0.35f, Screen.height * 0.125f), "Player name: ", guiStyle);
            username = GUI.TextField(new Rect(Screen.width * 0.5f, Screen.height * 0.1f, Screen.width * 0.35f, Screen.height * 0.125f), username, guiStyle);

            GUI.Label(new Rect(Screen.width * 0.15f, Screen.height * 0.3f, Screen.width * 0.35f, Screen.height * 0.125f), "Music: ", guiStyle);
           if(music == true)
               if (GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, Screen.width * 0.35f, Screen.height * 0.125f), "On", guiStyle))
            {
                music = false;
                AudioListener.volume = 0;
            }
            if (music == false)
                if (GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, Screen.width * 0.35f, Screen.height * 0.125f), "Off", guiStyle))
            {
                music = true;
                AudioListener.volume = 1;
            }
        }
        else if (pmenu == 3)
        {
            int scoresToShow = 10;
            DbHelper dbHelper = new DbHelper();
            List<ScoreModel> scores = dbHelper.GetScores(true, scoresToShow);

            GUIStyle headerStyle = new GUIStyle();
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.white;

            GUI.Box(new Rect(Screen.width * 0.25f, 0, Screen.width * 0.25f, Screen.height * 0.09f), "Player", guiStyle);
            GUI.Box(new Rect(Screen.width * 0.5f, 0, Screen.width * 0.25f, Screen.height * 0.09f), "Score", guiStyle);
            for (int i = 0; i < scoresToShow; i++)
            {
                GUI.Box(new Rect(Screen.width * 0.15f, Screen.height * (i + 1) * 0.09f, Screen.width * 0.35f, Screen.height * 0.09f), scores[i].Name, guiStyle);
                GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * (i + 1) * 0.09f, Screen.width * 0.35f, Screen.height * 0.09f), scores[i].Score.ToString(), guiStyle);
            }
        }
    }
    private void startGame(int gameLevel) {
        PlayerHealth.Health = 5;
        pmenu = 0;
        lv = gameLevel;
        Application.LoadLevel("Mapa");
        Affint.Start();

    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("Creating db if needed...");
        DbHelper dbHelper = new DbHelper();
        //dbHelper.CreateQuestionDatabase();
        //dbHelper.CreateScoreDatabase();
        dbHelper.Init();
        Debug.Log("Done!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
