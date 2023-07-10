using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public bool pausado {private set; get;}

    void Awake()
    {
        if(gameManager == null){
            gameManager = this;
        }
    }

    void Start(){
        
    }

    void Update()
    {
        
    }
}
