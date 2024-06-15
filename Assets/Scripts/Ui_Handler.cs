using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ui_Handler : MonoBehaviour
{
    MainData _instance;

    [SerializeField] TMP_Text playerName_MenuText;
    [SerializeField] TMP_Text playerScore_MenuText;
    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TMP_InputField playerInput;

    // Start is called before the first frame update

    private void Awake()
    {
        _instance = FindObjectOfType<MainData>();
    }

    void Start()
    {
        GetStoredName();
        GetStoredScore();
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        SetPlayerName(playerName);
        SetPlayerScore();
        playerInput.onEndEdit.AddListener(delegate { SetNewName(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    private void GetStoredName()
    {
        playerName = _instance.current_playerName;
    }

    private void GetStoredScore()
    {
        playerScore = _instance.current_playerScore;
    }

    private string SetNewName()
    {
        playerName= playerInput.text;
        _instance.new_playerName = playerName;
        return playerName;
    }

    private string SetPlayerName(string playerName)
    {
        if (playerName != null)
        {
            Debug.Log($"Name: {playerName} stored");
            return playerName_MenuText.text = "Best Player Name >>> " + playerName;

        }
        Debug.Log("No Name stored");
        playerName = playerInput.text;
        return playerName_MenuText.text = "Best Player Name >>> " + playerName;

    }

    private string SetPlayerScore()
    {
        if(playerScore.ToString() == null )
        {
            playerScore = 0;
            
        }

        return playerScore_MenuText.text = "Best Player Score >>> " + playerScore;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        _instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
