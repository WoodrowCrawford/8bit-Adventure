using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesBehavior : MonoBehaviour
{
   public GameManager gameManager;



    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameManager.Reset();
        }

    }

    //Play the animation for the death
    //Disable player controlls

    //Wait a few seconds then reset the game

}
