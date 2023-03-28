using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBuildingButton : MonoBehaviour
{
    public bool startbuilding;
    public void building()
    {
        if (startbuilding == false)
        {
            startbuilding = true;
        }
        else{
            startbuilding = false;
        }
        Debug.Log(startbuilding);
        
    }
    
}
