using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class QuestionManager {
    private int currentLevel;
    private List<QuestionModel> questions;

    private DbHelper dbHelper;

    private int currentQuestionIndex;

    public QuestionManager() {
        dbHelper = new DbHelper();
        currentLevel = 1;
        LoadQuestions();        
    }

    public void SetCurrentLevel(int level) {
        this.currentLevel = level;
    }

    public int GetCurrentLevel() {
        return this.currentLevel;
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
        questions = dbHelper.GetQuestionsFromLevel(this.currentLevel);
        if (questions.Count == 0) {
            throw new NoQuestionsException();
        }
    }
}
