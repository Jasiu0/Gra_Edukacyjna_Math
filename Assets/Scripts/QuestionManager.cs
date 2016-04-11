using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionManager {
    private int currentLevel;
    private List<QuestionModel> questions;

    private DbHelper dbHelper;

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

        return questions[0];
    }

    public bool IsCorrect(string chosenAnswer) {
        bool result = chosenAnswer.Equals(questions[0].GoodAnswer);
        questions.RemoveAt(0);

        return result;
    }


    private void LoadQuestions() {
        questions = dbHelper.GetQuestionsFromLevel(this.currentLevel);
        if (questions.Count == 0) {
            throw new NoQuestionsException();
        }
        ShuffleQuestionList();
    }

    private void ShuffleQuestionList() {
        //TODO implement shuffling
    }

}
