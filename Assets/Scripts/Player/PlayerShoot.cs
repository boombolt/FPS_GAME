using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No Camera Reference");
            this.enabled = false;
        }

        weaponManger = GetComponent<WeaponManager>();

        //weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);
    }

    private const string PLAYER_TAG = "Player";

    //[SerializeField]
    //private string weaponLayerName = "Weapon";

    private PlayerWeapon currentWeapon;

    private WeaponManager weaponManger;

    //[SerializeField]
    //private GameObject weaponGFX;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Update()
    {
        currentWeapon = weaponManger.GetCurrentWeapon();

        if (currentWeapon.fireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }

    }

    [Client]
    void Shoot()
    {
        Debug.Log("Shoot!");

        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask))
        {
            //Debug.Log("We Hit " + _hit.collider.name);

            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
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
