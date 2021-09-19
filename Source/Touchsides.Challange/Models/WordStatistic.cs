using System;
using System.Collections.Generic;
using System.Linq;

namespace Touchsides.Challange.Models
{
    public class WordStatistic
    {
        private char[] WordSplittingCharacters = @" .,?!;-:()[]{}“”'‘’*/\".ToCharArray();

        private readonly Dictionary<string, Statistic> _wordCollection = new Dictionary<string, Statistic>();
        private readonly Dictionary<char, int> _letterScores = new Dictionary<char, int>
        {
            { 'A', 1 }, { 'B', 3 }, { 'C', 3 }, { 'D', 2 }, { 'E', 1 }, { 'F', 4 }, { 'G', 2 }, { 'H', 4 }, { 'I', 1 },
            { 'J', 8 }, { 'K', 5 }, { 'L', 1 }, { 'M', 3 }, { 'N', 1 }, { 'O', 1 }, { 'P', 3 }, { 'Q', 10 }, { 'R', 1 },
            { 'S', 1 }, { 'T', 1 }, { 'U', 1 }, { 'V', 4 }, { 'W', 4 }, { 'X', 8 }, { 'Y', 4 }, { 'Z', 10 }
        };

        public void AddOrUpdateWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return;

            word = word.TrimPunctuation();
            word = word.NormalizeWord();

            if (string.IsNullOrWhiteSpace(word)) return;

            if (_wordCollection.TryGetValue(word, out var statistic))
            {
                statistic.IncrementApperanceCount();
                return;
            }            

            _wordCollection.Add(word, new Statistic(word, GetWordScore(word)));
        }

        public void AddOrUpdateWordsFromBookContents(string bookContents)
        {
            if (string.IsNullOrWhiteSpace(bookContents))
            {
                return;
            }

            var words = bookContents
                    .Replace(Environment.NewLine, " ")
                    .Split(WordSplittingCharacters, StringSplitOptions.RemoveEmptyEntries);

            for (int index = 0; index < words.Length; index++)
            {
                AddOrUpdateWord(words[index].ToUpperInvariant());
            }
        }


        private int GetWordScore(string word)
        {
            int score = 0;

            for (int index = 0; index < word.Length; index++)
            {
                var character = word[index];
                if (char.IsLetter(character))
                {
                    if (_letterScores.Keys.Contains(character))
                    {
                        score += _letterScores[character];
                    }                    
                }                
            }

            return score;
        }

        public override string ToString()
        {
            var mostFrequestWord = "";
            var mostFrequestWordCount = 0;

            var highScoringWord = "";
            var highScoringWordScore = 0;

            var frequent7CharaterWord = "";
            var frequent7CharaterWordCount = 0;

            if (_wordCollection != null && _wordCollection.Any())
            {
                var statistics = _wordCollection
                    .Select(words => words.Value)
                    .OrderByDescending(stats => stats.ApperanceCount)
                    .ToList();

                mostFrequestWord = statistics.FirstOrDefault()?.Word;
                mostFrequestWordCount = statistics.FirstOrDefault()?.ApperanceCount ?? 0;

                statistics = statistics
                    .OrderByDescending(stats => stats.Score)
                    .ToList();

                highScoringWord = statistics.FirstOrDefault()?.Word;
                highScoringWordScore = statistics.FirstOrDefault()?.ApperanceCount ?? 0;

                statistics = statistics
                    .Where(stats => stats.CharacterLength == 7)
                    .OrderByDescending(stats => stats.ApperanceCount)
                    .ToList();

                frequent7CharaterWord = statistics.FirstOrDefault()?.Word;
                frequent7CharaterWordCount = statistics.FirstOrDefault()?.ApperanceCount ?? 0;
            }

            return $"Most frequent word: \"{mostFrequestWord}\" occurred {mostFrequestWordCount} times{Environment.NewLine}" +
                   $"Most frequent 7-character word: \"{frequent7CharaterWord}\" occurred {frequent7CharaterWordCount} times{Environment.NewLine}" +
                   $"Highest scoring word(s) (according to the score table): \"{highScoringWord}\" with a score of {highScoringWordScore}{Environment.NewLine}";
        }
    }
}