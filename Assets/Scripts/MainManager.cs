using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public string currentBestPlayer_Name;
    public int currentBestPlayer_Score;
    public GameObject GameOverText;
    public Button backToMenu;
    public Button exitApp;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        backToMenu.onClick.AddListener(BackToMain);
        exitApp.onClick.AddListener(ExitGame);

        BestScoreUpdater(currentBestPlayer_Score, currentBestPlayer_Name);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            ComparePoints(currentBestPlayer_Score, m_Points, currentBestPlayer_Name);
            BestScoreUpdater(currentBestPlayer_Score, currentBestPlayer_Name);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void ComparePoints(int pointsToSave, int currentPoints, string playerName)
    {
        int storagePoints = MainData.Instance.current_playerScore;
        string storagePlayerName = MainData.Instance.new_playerName;

        if (currentPoints > storagePoints)
        {
            Debug.Log("Nuevo puntaje mayor...");
            pointsToSave = currentPoints;
            MainData.Instance.new_playerScore = pointsToSave;
            MainData.Instance.current_playerScore = pointsToSave;
            playerName = storagePlayerName;
            MainData.Instance.current_playerName = playerName;

            MainData.Instance.SaveData();
        }
        else
        {
            Debug.Log("Sin puntaje mayor...");
            return;
        }
    }

    private void BestScoreUpdater(int playerPoints, string playerName)
    {
        playerName = MainData.Instance.current_playerName;
        playerPoints = MainData.Instance.current_playerScore;
        bestScoreText.text = $"Best Score: {playerName} : {playerPoints}";
        Debug.Log("Score Billboard Updated...");
    }

    private void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
