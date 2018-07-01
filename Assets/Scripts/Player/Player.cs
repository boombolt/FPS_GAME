using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead =  false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    //[SerializeField]
    //private RectTransform playerHealthBar;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    private bool initialSetup = true;

    public void Setup()
    {
        CmdBroadcastPlayerSetup();
    }

    [Command]
    private void CmdBroadcastPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if(initialSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            initialSetup = false;
        }


        SetDefaults();
    }

    //private void Update()
    //{
    //    if(!isLocalPlayer)
    //    {
    //        if(Input.GetButtonDown(KeyCode.K))
    //        {
    //            RpcTakeDamage(100);
    //        }
    //    }
    //}

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead)
            return;


        currentHealth -= _amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health!");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is DEAD!");

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        Setup();

        Debug.Log(transform.name + " has RESPAWNED!");
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

    }

    public float GetCurrentHealth()
    {
        float _currentHealth = (float)currentHealth / 100;

        return _currentHealth;
    }

}
