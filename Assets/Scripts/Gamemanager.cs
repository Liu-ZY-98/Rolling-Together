using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Gamemanager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player1.GetComponent<player1controller>().enabled=true;
        player2.GetComponent<player2controller>().enabled=false;
        player3.GetComponent<player3controller>().enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("1"))
        {
            player1.GetComponent<player1controller>().enabled=true;
            player2.GetComponent<player2controller>().enabled=false;
            player3.GetComponent<player3controller>().enabled=false;
            
        }
        
        if(Input.GetKeyDown("2"))
        {   
            player1.GetComponent<player1controller>().enabled=false;
            player2.GetComponent<player2controller>().enabled=true;
            player3.GetComponent<player3controller>().enabled=false;
        
        }
        if(Input.GetKeyDown("3"))
        {
            player1.GetComponent<player1controller>().enabled=false;
            player2.GetComponent<player2controller>().enabled=false;
            player3.GetComponent<player3controller>().enabled=true;
        } 
    }
}

