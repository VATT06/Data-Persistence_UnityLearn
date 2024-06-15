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
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerScore;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TMP_InputField playerInput;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        playerInput.onEndEdit.AddListener(delegate { SetPlayerName(); });
        playerScore.text = SetPlayerScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    private string SetPlayerName()
    {
        playerName.text = "Player Name >>> "+MainData.Instance.playerName;
        if (playerName.text == null)
        {
            playerName.text = "Type your name...";
        }
        playerName.text = "Player Name >>> " + playerInput.text;
        
        return playerName.text;
    }

    private string SetPlayerScore()
    {
        playerScore.text = "PlayerScore >>> "+MainData.Instance.playerScore.ToString();
        if(playerScore.text == null )
        {
            playerScore.text = "PlayerScore >>> 0";
        }
        return playerScore.text;
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
