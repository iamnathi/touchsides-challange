namespace Touchsides.Challange.Models
{
    public class Statistic
    {
        public string Word { get; private set; }
        public int ApperanceCount { get; private set; }
        public int CharacterLength => Word.Length;
        public int Score { get; private set; }

        public Statistic(string word, int score)
        {
            Word = word;
            ApperanceCount = 1;
            Score = score;
        }

        public void IncrementApperanceCount()
        {
            ApperanceCount++;
        }
    }
}
