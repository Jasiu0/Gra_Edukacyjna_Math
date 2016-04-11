public class QuestionModel {
    public int Id;
    public int Level;
    public string Question;
    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string GoodAnswer;

    public QuestionModel() {
        this.Id = -1;
        this.Level = -1;
        this.Question = "";
        this.Answer1 = "";
        this.Answer2 = "";
        this.Answer3 = "";
        this.GoodAnswer = "";
    }
}
