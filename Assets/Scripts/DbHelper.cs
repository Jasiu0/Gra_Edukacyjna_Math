using UnityEngine;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class DbHelper {
    private int[] LEVEL_QUESTION_COUNTS = new int[8]{ 351, 351, 113, 88, 351, 351, 123, 119 }; 

    private string questionDbUri;
    private string scoreDbUri;

    public DbHelper() {
        //Uncomment following 2 lines when debugging on PC
        //this.questionDbUri = "URI=file:" + Application.dataPath + "/QuestionDb.sqlite";
        //this.scoreDbUri = "URI=file:" + Application.dataPath + "/ScoreDb.sqlite";


        //Uncomment following 2 lines when debugging on Android Device
        this.questionDbUri = "URI=file:" + Application.persistentDataPath + "/QuestionDb.sqlite";
        this.scoreDbUri = "URI=file:" + Application.persistentDataPath + "/ScoreDb.sqlite";
        
        //Uncomment when building first time on Android device
        //CreateQuestionDatabase();  
        //CreateScoreDatabase();
    }   
    
    public void Init() {
        using (IDbConnection dbConnection = new SqliteConnection(questionDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                try {
                    string sqlQuery = "SELECT * FROM Data WHERE id = 0";
                    dbCommand.CommandText = sqlQuery;
                    dbCommand.ExecuteReader();
                } catch (SqliteException e) {
                    Debug.Log(e.ToString());
                    CreateQuestionDatabase();
                }

            }
        }

        using (IDbConnection dbConnection = new SqliteConnection(scoreDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                try {
                    string sqlQuery = "SELECT * FROM Data WHERE id = 0";
                    dbCommand.CommandText = sqlQuery;
                    dbCommand.ExecuteReader();
                } catch (SqliteException e) {
                    Debug.Log(e.ToString());
                    CreateScoreDatabase();
                }

            }
        }
    }


    public void CreateQuestionDatabase() {
        using (IDbConnection dbConnection = new SqliteConnection(questionDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                CreateQuestionDataTable(dbCommand);
                //ClearDb(dbCommand);

                CreateAdditionQuestions(dbCommand, 0, 25);
                CreateSubtractionQuestions(dbCommand, 0, 25);
                CreateMultiplicationLevelDb(dbCommand, 0, 25);
                CreateDivisionLevelDb(dbCommand, 0,25);
                CreateAdditionQuestions(dbCommand, 25, 50);
                CreateSubtractionQuestions(dbCommand, 25, 50);
                CreateMultiplicationLevelDb(dbCommand, 25, 50);
                CreateDivisionLevelDb(dbCommand, 25, 50);
            }
            dbConnection.Close();
        }
    }

    public void CreateScoreDatabase() {
        using (IDbConnection dbConnection = new SqliteConnection(scoreDbUri)) {
            dbConnection.Open();

            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                string sqlQuery = "CREATE TABLE if not exists'Data' (" +
	                              "'id' INTEGER PRIMARY KEY AUTOINCREMENT," +
	                              "'name'  TEXT NOT NULL," +
	                              "'score' INTEGER NOT NULL); ";
                dbCommand.CommandText = sqlQuery;
                dbCommand.ExecuteScalar();
            }
            dbConnection.Close();
        }
    }

    public List<QuestionModel> GetQuestionsFromLevel(int level) {
        List<QuestionModel> questionList = new List<QuestionModel>();

        using (IDbConnection dbConnection = new SqliteConnection(questionDbUri)) {
            dbConnection.Open();
            int count = -1;
            using (IDbCommand dbCommand = dbConnection.CreateCommand()) {
                for (int levelToRead = level; levelToRead > 0; levelToRead--) {
                    LoadQuestionsFromLevel(dbCommand, levelToRead, questionList, count);
                    if (count == -1) {
                        count = 50;
                    } else {
                        count /= 2;
                    }
                }
                dbConnection.Close();
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

    public List<ScoreModel> GetScores(bool sorted, int count) {
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
            dbConnection.Close();
        }
        if (scoreList.Count < count) {
            for (int i = 0; i < count - scoreList.Count; i++) {
                AddScoreRecord("Player", 0);
            }
            scoreList = GetScores(true, count);
        }

        if (sorted) {
            scoreList.Sort();
            scoreList.Reverse();
        }
        scoreList = scoreList.GetRange(0, count);
        return scoreList;
    }


    private void LoadQuestionsFromLevel(IDbCommand dbCommand, int level, List<QuestionModel> questionList, int count) {
        string sqlQuery = String.Format("SELECT * FROM Data WHERE lvl == \"{0}\"", level);

        dbCommand.CommandText = sqlQuery;

        using (IDataReader dataReader = dbCommand.ExecuteReader()) {
            System.Random random = new System.Random();
            int rowCounter = 0;
            while (dataReader.Read()) {
                if (count != -1) {
                    double decision = random.Next(LEVEL_QUESTION_COUNTS[level - 1]);
                    if (decision > count) {
                        continue;
                    }
                }
                QuestionModel question = new QuestionModel();

                question.Id = dataReader.GetInt32(0);
                question.Level = dataReader.GetInt32(1);
                question.Question = dataReader.GetString(2);
                question.Answer1 = dataReader.GetString(3);
                question.Answer2 = dataReader.GetString(4);
                question.Answer3 = dataReader.GetString(5);
                question.GoodAnswer = dataReader.GetString(6);

                questionList.Add(question);
                rowCounter++;
            }
            Debug.Log("Added " + rowCounter + " new questions from level " + level);
            dataReader.Close();
        }
    }
    
    private void ClearDb(IDbCommand dbCommand) {
        string sqlQuery = "DELETE FROM Data";
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();

        sqlQuery = "DELETE FROM SQLITE_SEQUENCE WHERE name == \'Data\'";
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();
    }

    private void CreateQuestionDataTable(IDbCommand dbCommand) {
        string sqlQuery = "CREATE TABLE if not exists 'Data' (" +
                          "'id'	INTEGER PRIMARY KEY AUTOINCREMENT," +
	                      "'lvl'	INTEGER NOT NULL," +
	                      "'question'	TEXT NOT NULL," +
	                      "'answer1'	TEXT NOT NULL," +
	                      "'answer2'	TEXT NOT NULL," +
	                      "'answer3'	TEXT NOT NULL," +
	                      "'goodanswer'	TEXT NOT NULL); ";
        dbCommand.CommandText = sqlQuery;
        dbCommand.ExecuteScalar();
    }
    
    private void CreateAdditionQuestions(IDbCommand dbCommand, int minResult, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = minResult == 0 ? "1" : "5";
        for (int i = minResult; i <= maxResult; i++) { 
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

    private void CreateSubtractionQuestions(IDbCommand dbCommand, int minResult, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = minResult == 0 ? "2" : "6";
        for (int i = minResult; i <= maxResult; i++) {
            for (int j = 0; j <= i - minResult; j++) {
                question = i + " - " + j;
                goodAnswer = (i - j) + "";

                goodAnswerIndex = random.Next(3);
                answers[goodAnswerIndex] = goodAnswer;

                offsets.Add(0);
                offsets.Add(0);

                do {
                    offsets[0] = random.Next(11) - 5;
                } while (offsets[0] == 0 || i - j + offsets[0] < 0);

                do {
                    offsets[1] = random.Next(11) - 5;
                } while (offsets[0] == offsets[1] || offsets[1] == 0 || i - j + offsets[1] < 0);

                for (int k = 0; k < 3; k++) {
                    if (k != goodAnswerIndex) {
                        answers[k] = (i - j + offsets[0]) + "";
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

    private void CreateMultiplicationLevelDb(IDbCommand dbCommand, int minResult, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = minResult == 0 ? "3" : "7";
        int i = minResult == 0? 0 : 1;
        while (i <= maxResult) {
            int j;
            if (i == 0) {
                j = 0;
            } else {
                j = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(minResult) / Convert.ToDouble(i)));
            }
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

    private void CreateDivisionLevelDb(IDbCommand dbCommand, int minResult, int maxResult) {
        string level = "", question = "", goodAnswer = "";
        string[] answers = { "", "", "" };
        int goodAnswerIndex;
        List<int> offsets = new List<int>();

        System.Random random = new System.Random();

        level = minResult == 0? "4" : "8";
        for (int i = minResult + 1; i <= maxResult; i++){
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
