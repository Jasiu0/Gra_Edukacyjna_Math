using UnityEngine;
using System.Collections;

public class QuestionView : MonoBehaviour {
    private Rect questionRect;
    private Rect answer1Rect;
    private Rect answer2Rect;
    private Rect answer3Rect;
    private GUIStyle centeredStyle;

    private QuestionManager manager;
    private QuestionModel currentQuestion;

    private GameObject background;

    private GUIStyle guiStyle;
    void Start() {

        questionRect = new Rect(0, Screen.height * 0.78f,
            Screen.width * 0.85f, Screen.height * 0.11f);
        answer1Rect = new Rect(0, Screen.height * 0.89f,
            Screen.width / 3, Screen.height * 0.11f);
        answer2Rect = new Rect(Screen.width / 3, Screen.height * 0.89f,
            Screen.width / 3, Screen.height * 0.11f);
        answer3Rect = new Rect(2 * Screen.width / 3, Screen.height * 0.89f,
            Screen.width / 3, Screen.height * 0.11f);


        manager = new QuestionManager();
        currentQuestion = new QuestionModel();

        background = GameObject.FindWithTag("floorBackground");
    }
   
    void OnGUI() {
       
        guiStyle = new GUIStyle(GUI.skin.button);
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 30;  //must be int, obviously...
        guiStyle.alignment = TextAnchor.MiddleCenter;

        if (Options_click.Option == 0)
        {
            GUI.Box(questionRect, currentQuestion.Question, guiStyle);

            if (GUI.Button(answer1Rect, currentQuestion.Answer1, guiStyle))
            {
                CsvLogger.LogEvent("Answer 1 chosen");
                verifyAnswer(currentQuestion.Answer1);
            }

            if (GUI.Button(answer2Rect, currentQuestion.Answer2, guiStyle))
            {
                CsvLogger.LogEvent("Answer 2 chosen");
                verifyAnswer(currentQuestion.Answer2);
            }

            if (GUI.Button(answer3Rect, currentQuestion.Answer3, guiStyle))
            {
                CsvLogger.LogEvent("Answer 3 chosen");
                verifyAnswer(currentQuestion.Answer3);
            }
        }
    }   

    public void ShowQuestion() {
        try {
            currentQuestion = manager.GetNewQuestion();
        } catch (NoQuestionsException e) {
            Debug.LogError("No Question Exception");
        }

    }

    public void OnNewLevel() {
        manager.ReloadQuestions();
    }

    private void verifyAnswer(string chosenAnswer) {
        /* if (manager.IsCorrect(chosenAnswer)) {
             //Detonate.Answer = 1;
             //Debug.Log("Correct!");
         } else {
             //Debug.Log("Wrong Answer!");
         }*/
        bool doExplode = manager.IsCorrect(chosenAnswer);
        CsvLogger.LogEvent(doExplode ? "Good answer" : "Wrong answer");
        background.SendMessage("QuestionAnswered");
        background.SendMessage("Explode", doExplode);

        //TODO Do the styling stuff
        currentQuestion = new QuestionModel();
    }
}
