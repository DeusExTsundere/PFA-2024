using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(menuName = "leaderboard")]
public class leaderboard : ScriptableObject
{
    [SerializeField] private List<int> leaderboards;
    [SerializeField] private List<string> leaderboardNames;


    public List<int> Score
    {
        get { return leaderboards; }
        set { leaderboards = value; }
    }

    public List<string> ScoreName
    { 
        get { return leaderboardNames; } 
        set { leaderboardNames = value; }
    }

}
