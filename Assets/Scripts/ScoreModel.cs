using System;

public class ScoreModel : IComparable<ScoreModel> {
    public int Id;
    public string Name;
    public int Score;

    public ScoreModel() {
        this.Id = -1;
        this.Name = "";
        this.Score = -1;
    }

    public int CompareTo(ScoreModel other) {
        return Score.CompareTo(other.Score);
    }
}