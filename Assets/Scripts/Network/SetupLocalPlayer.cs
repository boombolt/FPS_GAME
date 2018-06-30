using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class SetupLocalPlayer : NetworkBehaviour {

    Camera sceneCamera;

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGFX;

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    void Start ()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            SetLayerRecursively(playerGFX, LayerMask.NameToLayer(dontDrawLayerName));

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUI playerUI = playerUIInstance.GetComponent<PlayerUI>();
            if (playerUI == null)
            {
                Debug.LogError("NO PLAYERUI COMPONENT ON PLAYER UI PREFAB");
            }

            playerUI.SetPlayerReference(GetComponent<Player>());

            GetComponent<Player>().Setup();

        }

        
	}

    void SetLayerRecursively(GameObject _object, int _newLayer)
    {
        _object.layer = _newLayer;

        foreach (Transform child in _object.transform)
        {
            SetLayerRecursively(child.gameObject, _newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(playerUIInstance);

        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }


}
