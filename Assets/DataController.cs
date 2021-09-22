using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour

{
    public static DataController instance;
    public static string pName;
    public int highScore;
    public  InputField nameField;
    public Text rankText;
    public string highScoreName;
    
    
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadNameAndScore();
       
        rankText.text = highScoreName+" : "+highScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        pName = nameField.text;
      
    }

    // Update is called once per frame
    void Update()
    {
       
        if (nameField != null)
        {
            pName = nameField.text;
        }
        else
        {
            pName = GameObject.FindObjectOfType<NameFieldHold>().nameToKeep.ToString();
        }





    }
    [System.Serializable]
    class SaveInformation
    {
        public string pName;
        public int highScore;
    }
    public void SaveNameAndScore()
    {
        SaveInformation saveInformation = new SaveInformation();
        saveInformation.pName = pName;
        saveInformation.highScore = MainManager.m_Points;
        string json = JsonUtility.ToJson(saveInformation);
        File.WriteAllText(Application.persistentDataPath + "/SaveFile.json", json);
    }
    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/SaveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveInformation saveInformation = JsonUtility.FromJson<SaveInformation>(json);
            highScoreName = saveInformation.pName;
            highScore = saveInformation.highScore;
            rankText.text = highScoreName +" : "+ "highScore :" + highScore;
        }
        
    }
    //public void SaveUsername()
    //{
    //      PlayerPrefs.SetString("Username",nameField.text);
    //}

    //public void LoadUsername()
    //{
    //    if (PlayerPrefs.HasKey("Username"))
    //    {
    //        nameField.text = PlayerPrefs.GetString("Username");
    //    }
    //    else
    //    {
    //        Debug.Log("Username not saved yet");
    //    }
    //}
}
