using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    [Header("UI Element")]
    public TMP_Text _txtHealth;
    public GameObject _gameOver;
    public GameObject _restartButton;

    // Start is called before the first frame update
    void Start()
    {
        _gameOver.SetActive(false);
        _restartButton.SetActive(false);
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthUpdated += OnHealthUpdated;  //subscribe to the C# action
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthUpdated -= OnHealthUpdated; //Unsubscribe to C# action
    }

    void OnHealthUpdated(float health)
    {
        _txtHealth.text = "Health : " + Mathf.Floor(health).ToString(); //floor rounds down. ceil would round up
    }

    void OnDeath()
    {
        _gameOver.SetActive(true);
        _restartButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
