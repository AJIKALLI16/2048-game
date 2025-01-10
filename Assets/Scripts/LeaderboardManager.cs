using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Dan.Main;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject LeaderboardPanel;
    [SerializeField]
    private Button LeaderbordButton;
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    [SerializeField]
    private TextMeshProUGUI idPlayer;

    private string publicLeaderboardKey = "4eed00ba1365f05a278e17c6684d6c407ed8aed95e8e759ba00cbc2eb132d347";

    private void Start()
    {
        GetLeaderboard();
        LeaderboardPanel.SetActive(false);

        LeaderbordButton.onClick.AddListener(ToggleLeaderboard);
        
    }

    public void ToggleLeaderboard()
    {
        LeaderboardPanel.SetActive(!LeaderboardPanel.activeSelf);
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int LoopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < LoopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }
    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
