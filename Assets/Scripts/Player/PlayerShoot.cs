﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No Camera Reference");
            this.enabled = false;
        }
    }

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            //Debug.Log("We Hit " + _hit.collider.name);

            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " was shot!");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }
}
