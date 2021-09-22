using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Button quitButton;
    
    private bool m_Started = false;
    public static int m_Points;
  
    public bool m_GameOver = false;
    
    public string pName;
   


    // Start is called before the first frame update
    void Start()
    {
        quitButton.gameObject.SetActive(false);
        LoadBrick();
        
       
    }

    private void Update()
    {

        if (GameObject.FindGameObjectWithTag("Brick")==null)
        {
            LoadBrick();
        }

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
           
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
           
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                m_Points = 0;
                GameObject.FindObjectOfType<DataController>().LoadNameAndScore();
            }
        }
       
        if (m_Points>GameObject.FindObjectOfType<DataController>().highScore)
        {

            UpdateScoreBord();
        }
        pName = DataController.pName;
    }

    void AddPoint(int point)
    {
        m_Points += point;
    
        ScoreText.text = pName + ": " + $" Score : {m_Points}";
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    void UpdateScoreBord()
    {
        GameObject.FindObjectOfType<DataController>().SaveNameAndScore();
        GameObject.FindObjectOfType<DataController>().LoadNameAndScore();
    }
    void LoadBrick()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

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
    public void QuitOnclick()
    {
        if (m_Points == GameObject.FindObjectOfType<DataController>().highScore)
        {
            GameObject.FindObjectOfType<DataController>().SaveNameAndScore();
            

        }
    }
    public void Quit()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#elif (UNITY_WEBGL)
    //Application.QpenUrl(
#endif
    }


}  
