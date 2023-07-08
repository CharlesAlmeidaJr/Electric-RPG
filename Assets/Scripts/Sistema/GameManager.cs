using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

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
