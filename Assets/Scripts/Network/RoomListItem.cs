using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinMatchDelegate(MatchInfoSnapshot _match);
    private JoinMatchDelegate joinMatchCallback;

    [SerializeField]
    private Text roomNameText;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot _match, JoinMatchDelegate _joinMatchCallback)
    {
        match = _match;
        joinMatchCallback = _joinMatchCallback;
        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinMatch()
    {
        joinMatchCallback.Invoke(match);
    }
}
