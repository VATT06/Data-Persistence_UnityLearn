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
    [SerializeField] TMP_Text playerName_MenuText;
    [SerializeField] TMP_Text playerScore_MenuText;
    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TMP_InputField playerInput;

    // Start is called before the first frame update

   
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
        playerName = MainData.Instance.playerName;
    }

    private void GetStoredScore()
    {
        playerScore = MainData.Instance.playerScore;
    }

    private string SetNewName()
    {
        playerName= playerInput.text;
        MainData.Instance.playerName = playerName;
        return playerName;
    }

    private string SetPlayerName(string playerName)
    {
        if (playerName != null)
        {
            Debug.Log($"Name: {playerName} stored");
            return playerName_MenuText.text = "Player Name >>> " + playerName;

        }
        Debug.Log("No Name stored");
        playerName = playerInput.text;
        return playerName_MenuText.text = "Player Name >>> " + playerName;

    }

    private string SetPlayerScore()
    {
        if(playerScore.ToString() == null )
        {
            playerScore = 0;
            
        }

        return playerScore_MenuText.text = "PlayerScore >>> " + playerScore;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        MainData.Instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
