using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    private void Start()
    {
        InGameMenu.IsOn = false;
    }

    [SerializeField]
    private RectTransform playerHealthBar;

    [SerializeField]
    private Image playerHealthBarImage;

    [SerializeField]
    private GameObject inGameMenu;

    private Player player;

    private void Update()
    {
        SetPlayerHealthBar(player.GetCurrentHealth());

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleInGameMenu();
        }
    }

    void ToggleInGameMenu()
    {
        inGameMenu.SetActive(!inGameMenu.activeSelf);
        InGameMenu.IsOn = inGameMenu.activeSelf;
    }

    public void SetPlayerReference(Player _player)
    {
        player = _player;
    }

    public void SetPlayerHealthBar(float _currentHealth)
    {
        playerHealthBar.localScale = new Vector3(_currentHealth, 1f, 1f);

        if(_currentHealth <= 0.3f)
        {
            playerHealthBarImage.color = Color.red;
        }
        else
        {
            playerHealthBarImage.color = Color.white;
        }
        
    }

}
