using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using System;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private TextMeshProUGUI score;
        public gameController gameController;
        // ------------------------------------------------------------

        private void Start()
        {
             LoadEntries();

        }

        public void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>

            Leaderboards.PositronLeaderboard1.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    if(i == 0 || i == 2 || i == 4)
                    {
                        _entryTextObjects[i].color = new Color32(225, 111, 104, 255);
                    }
                    else
                    {
                        _entryTextObjects[i].color = new Color32(104, 128, 225, 255);

                    }
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
                    bool isMine = entries[i].IsMine();

                    if (isMine)
                    {
                        _entryTextObjects[i].color = Color.green;
                        gameController.highscore = entries[i].Score;
                    }
                }
            });
        }

        public void UploadEntry()
        {
            if(_usernameInputField != null)
            {
                Leaderboards.PositronLeaderboard1.UploadNewEntry(_usernameInputField.text, int.Parse(score.text), isSuccessful =>
                {
                    if (isSuccessful)
                        LoadEntries();
                }, errorMessage => Debug.LogError(errorMessage));
            }
            
        }
    }
}