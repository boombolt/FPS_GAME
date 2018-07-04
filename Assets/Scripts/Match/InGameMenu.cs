using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class InGameMenu : MonoBehaviour {

    public void Start()
    {
        networkManager = NetworkManager.singleton;
        IsOn = false;
    }

    public static bool IsOn = false;

    private NetworkManager networkManager;

    public void LeaveMatch()
    {
        MatchInfo matchInfo = networkManager.matchInfo;

        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
