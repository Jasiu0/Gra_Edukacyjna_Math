using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class DbHelper {
    private string dbUri;

    public DbHelper() {
        this.dbUri = "URI=file:" + Application.dataPath + "/Database/QuestionDb.sqlite";
    }

    public List<QuestionModel> GetQuestionsFromLevel(int level) {
        List<QuestionModel> questionList = new List<QuestionModel>();

        using (IDbConnection dbConnection = new SqliteConnection(dbUri)) {
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
}
