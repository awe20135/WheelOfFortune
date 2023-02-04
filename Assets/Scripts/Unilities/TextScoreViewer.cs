using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.Utilities
{
    class TextScoreViewer : MonoBehaviour, IScoreViewer
    {
        struct ScorePostfix
        {
            public char postfix;
            public int upperBound;
        }

        private Text _scoreText;
        private readonly List<ScorePostfix> _scorePostfixes = new List<ScorePostfix>()
        {
            new ScorePostfix(){postfix = ' ', upperBound = 1_000},
            new ScorePostfix(){postfix = 'k', upperBound = 1_000_000},
            new ScorePostfix(){postfix = 'm', upperBound = 1_000_000_000},
        };

        private void Start()
        {
            _scoreText = GetComponent<Text>();
            if (!_scoreText)
                Debug.LogError($"Undefined Text component on {this}");
        }

        public void ShowScore(int scoreValue)
        {
            _scoreText.text = GetScoreString(scoreValue);
        }

        private string GetScoreString(int score)
        {
            if (score >= _scorePostfixes[_scorePostfixes.Count - 1].upperBound)
            {
                return $"> 1 000m";
            }

            string formatedString = "";

            for (int postfixIndex = 0; postfixIndex < _scorePostfixes.Count; postfixIndex++)
            {
                if (score < _scorePostfixes[postfixIndex].upperBound)
                {
                    float valueToShow = postfixIndex > 0 ? (float)score / (float)_scorePostfixes[postfixIndex - 1].upperBound : score;
                    formatedString = $"{valueToShow.ToString("0.00")}{_scorePostfixes[postfixIndex].postfix}";
                    break;
                }
            }

            return formatedString;
        }
    }
}
