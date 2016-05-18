using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuestionManager {
    private List<QuestionModel> questions;

    private DbHelper dbHelper;

    private int currentQuestionIndex;

    public QuestionManager() {
        dbHelper = new DbHelper();
        LoadQuestions();        
    }

    public void ReloadQuestions() {
        Debug.Log("Reloading questions for level " + MainMenu.lv);
        questions.Clear();
        LoadQuestions();
    }

    public QuestionModel GetNewQuestion() {
        if (questions.Count == 0) {
            LoadQuestions();
        }

        System.Random random = new System.Random();
        currentQuestionIndex = random.Next(questions.Count);
        return questions[currentQuestionIndex];
    }

    public bool IsCorrect(string chosenAnswer) {
        bool result = chosenAnswer.Equals(questions[currentQuestionIndex].GoodAnswer);
        questions.RemoveAt(currentQuestionIndex);

        return result;
    }


    private void LoadQuestions() {
        questions = dbHelper.GetQuestionsFromLevel(MainMenu.lv);
        if (questions.Count == 0) {
            throw new NoQuestionsException();
        }
    }
}
