using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameFieldHold :MonoBehaviour
{
    public  string nameToKeep;
    // Start is called before the first frame update
    
    public void KeepName()
     {
        nameToKeep = DataController.pName.ToString();
     }
    public void UpDate()
    {
        Debug.Log(nameToKeep);
    }

}
