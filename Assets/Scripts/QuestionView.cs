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
    void Start() {

        questionRect = new Rect(0, Screen.height - 90f,
            Screen.width * 0.85f, 40);
        answer1Rect = new Rect(0, Screen.height - 50f,
            Screen.width / 3, 50);
        answer2Rect = new Rect(Screen.width / 3, Screen.height - 50,
            Screen.width / 3, 50);
        answer3Rect = new Rect(2 * Screen.width / 3, Screen.height - 50,
            Screen.width / 3, 50);


        manager = new QuestionManager();
        currentQuestion = new QuestionModel();

        background = GameObject.FindWithTag("floorBackground");
    }
   
    void OnGUI() {

        if (Options.Option == 0)
        {
            GUI.Box(questionRect, currentQuestion.Question);

            if (GUI.Button(answer1Rect, currentQuestion.Answer1))
            {
                verifyAnswer(currentQuestion.Answer1);
            }

            if (GUI.Button(answer2Rect, currentQuestion.Answer2))
            {
                verifyAnswer(currentQuestion.Answer2);
            }

            if (GUI.Button(answer3Rect, currentQuestion.Answer3))
            {
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
        background.SendMessage("Explode", doExplode);

        //TODO Do the styling stuff
        currentQuestion = new QuestionModel();
    }
}
