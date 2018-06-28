using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SetupLocalPlayer : NetworkBehaviour {

    public Camera thisCamera;

	void Start ()
    {
        if (!isLocalPlayer)
        {
            thisCamera.enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerHealth>().enabled = false;
            GetComponentInChildren<RaycastShoot>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;

        }
	}
	

}
