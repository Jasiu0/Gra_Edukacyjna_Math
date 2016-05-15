using UnityEngine;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class DbHelper {
    private string questionDbUri;
    private string scoreDbUri;

    public DbHelper() {
        this.questionDbUri = "URI=file:" + Application.dataPath + "/Database/QuestionDb.sqlite";
        this.scoreDbUri = "URI=file:" + Application.dataPath + "/Database/ScoreDb.sqlite";        
    }     

    public void CreateQuestionDatabase() {
        using (IDbConnection dbConnection = new SqliteConnection(questionDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                ClearDb(dbCommand);

                CreateAdditionLevelDb(dbCommand, 50);
                CreateMultiplicationLevelDb(dbCommand, 50);
                CreateDivisionLevelDb(dbCommand, 50);
            }
            dbConnection.Close();
        }
    }

    public List<QuestionModel> GetQuestionsFromLevel(int level) {
        List<QuestionModel> questionList = new List<QuestionModel>();

        using (IDbConnection dbConnection = new SqliteConnection(questionDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                string sqlQuery = String.Format("SELECT * FROM Data WHERE lvl == \"{0}\"", level);

                dbCommand.CommandText = sqlQuery;

                using (IDataReader dataReader = dbCommand.ExecuteReader()) {
                    while (dataReader.Read()) {
                        QuestionModel question = new QuestionModel();

                        question.Id = dataReader.GetInt32(0);
                        question.Level = dataReader.GetInt32(1);
                        question.Question = dataReader.GetString(2);
                        question.Answer1 = dataReader.GetString(3);
                        question.Answer2 = dataReader.GetString(4);
                        question.Answer3 = dataReader.GetString(5);
                        question.GoodAnswer = dataReader.GetString(6);

                        questionList.Add(question);
                    }
                    dbConnection.Close();
                    dataReader.Close();
                }
            }
        }
        return questionList;
    }

    public void ClearScoreDatabase() {
        using (IDbConnection dbConnection = new SqliteConnection(scoreDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                ClearDb(dbCommand);
            }
            dbConnection.Close();
        }
    }

    public void AddScoreRecord(string name, int score) {
        using (IDbConnection dbConnection = new SqliteConnection(scoreDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                string sqlQuery = String.Format("INSERT INTO Data(name, score) VALUES(\"{0}\", \"{1}\")", name, score);
                dbCommand.CommandText = sqlQuery;
                Debug.Log("Writing new score...");
                dbCommand.ExecuteScalar();
            }
            Debug.Log("New score written!");
            dbConnection.Close();
        }
    }

    public List<ScoreModel> GetScores() {
        List<ScoreModel> scoreList = new List<ScoreModel>();

        using (IDbConnection dbConnection = new SqliteConnection(scoreDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                string sqlQuery = "SELECT * FROM Data";
                dbCommand.CommandText = sqlQuery;

                using (IDataReader dataReader = dbCommand.ExecuteReader()) {
                    while (dataReader.Read()) {
                        ScoreModel score = new ScoreModel();

                        score.Id = dataReader.GetInt32(0);
                        score.Name = dataReader.GetString(1);
                        score.Score = dataReader.GetInt32(2);

                        scoreList.Add(score);
                    }
                }
            }
        }
        return scoreList;
    }


    private void ClearDb(IDbCommand dbCommand) {
        string sqlQuery = "DELETE FROM Data";
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();

        sqlQuery = "DELETE FROM SQLITE_SEQUENCE WHERE name == \'Data\'";
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();
    }

    private void CreateAdditionLevelDb(IDbCommand dbCommand, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = "1";
        for (int i = 0; i <= maxResult; i++) {
            for (int j = 0; j <= maxResult - i; j++) {
                question = i + " + " + j;
                goodAnswer = (i + j) + "";

                goodAnswerIndex = random.Next(3);
                answers[goodAnswerIndex] = goodAnswer;

                offsets.Add(0);
                offsets.Add(0);

                do {
                    offsets[0] = random.Next(11) - 5;
                } while (offsets[0] == 0 || i + j + offsets[0] < 0);

                do {
                    offsets[1] = random.Next(11) - 5;
                } while (offsets[0] == offsets[1] || offsets[1] == 0 || i + j + offsets[1] < 0);

                for (int k = 0; k < 3; k++) {
                    if (k != goodAnswerIndex) {
                        answers[k] = (i + j + offsets[0]) + "";
                        offsets.RemoveAt(0);
                    }
                }

                string sqlQuery = String.Format("INSERT INTO Data(lvl, question, answer1, answer2, answer3, goodanswer) " +
                    "VALUES(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\")",
                    level, question, answers[0], answers[1], answers[2], goodAnswer);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
            }
        }
    }

    private void CreateMultiplicationLevelDb(IDbCommand dbCommand, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = "2";
        int i = 0;
        while (i <= maxResult) {
            int j = 0;
            do {
                question = i + " * " + j;
                goodAnswer = (i * j) + "";

                goodAnswerIndex = random.Next(3);
                answers[goodAnswerIndex] = goodAnswer;

                offsets.Add(0);
                offsets.Add(0);

                do {
                    offsets[0] = random.Next(11) - 5;
                } while (offsets[0] == 0 || i * j + offsets[0] < 0);

                do {
                    offsets[1] = random.Next(11) - 5;
                } while (offsets[0] == offsets[1] || offsets[1] == 0 || i * j + offsets[1] < 0);

                for (int k = 0; k < 3; k++) {
                    if (k != goodAnswerIndex) {
                        answers[k] = (i * j + offsets[0]) + "";
                        offsets.RemoveAt(0);
                    }
                }

                string sqlQuery = String.Format("INSERT INTO Data(lvl, question, answer1, answer2, answer3, goodanswer) " +
                    "VALUES(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\")",
                    level, question, answers[0], answers[1], answers[2], goodAnswer);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();

                j++;
            } while (i * j <= maxResult && i != 0);
            i++;
        }
    }

    private void CreateDivisionLevelDb(IDbCommand dbCommand, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = "3";
        for (int i = 1; i <= maxResult; i++){
            for (int j = maxResult; j > 0; j--) {
                if ((i % j) != 0) {
                    continue;
                }
                question = i + " / " + j;
                goodAnswer = (i / j) + "";

                goodAnswerIndex = random.Next(3);
                answers[goodAnswerIndex] = goodAnswer;

                offsets.Add(0);
                offsets.Add(0);

                do {
                    offsets[0] = random.Next(11) - 5;
                } while (offsets[0] == 0 || i / j + offsets[0] < 0);

                do {
                    offsets[1] = random.Next(11) - 5;
                } while (offsets[0] == offsets[1] || offsets[1] == 0 || i / j + offsets[1] < 0);

                for (int k = 0; k < 3; k++) {
                    if (k != goodAnswerIndex) {
                        answers[k] = (i / j + offsets[0]) + "";
                        offsets.RemoveAt(0);
                    }
                }

                string sqlQuery = String.Format("INSERT INTO Data(lvl, question, answer1, answer2, answer3, goodanswer) " +
                    "VALUES(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\")",
                    level, question, answers[0], answers[1], answers[2], goodAnswer);
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
            }
        }
    }
}
