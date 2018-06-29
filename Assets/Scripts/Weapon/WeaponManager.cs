using UnityEngine.Networking;
using UnityEngine;

public class WeaponManager : NetworkBehaviour {

    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponInstance = (GameObject)Instantiate(_weapon.weaponGFX, weaponHolder.position, weaponHolder.rotation);
        _weaponInstance.transform.SetParent(weaponHolder);

        if (isLocalPlayer)
            _weaponInstance.layer = LayerMask.NameToLayer(weaponLayerName);
    }

}
