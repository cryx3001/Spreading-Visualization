using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBarPos : MonoBehaviour
{
    private Vector3 camBord;
    
    void Update()
    {
        camBord = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(camBord[0]*0.6f,0,0);
        transform.localScale = new Vector3(camBord[0]*0.01f, camBord[1]*2, 1);
    }
}
