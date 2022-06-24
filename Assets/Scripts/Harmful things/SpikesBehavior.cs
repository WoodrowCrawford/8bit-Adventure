using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float _respawnTimeAmount = 0;



    //The respawn timer
    IEnumerator RespawnTime(float ResetTime)
    {
        _respawnTimeAmount = ResetTime;
        yield return new WaitForSeconds(ResetTime);

    }
    ///Get an on trigger for when the player touches the spikes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Play the death animation and disable controls

            SceneManager.LoadScene("Level 1");
        }

           
        
    }

    //Play the animation for the death
    //Disable player controlls

    //Wait a few seconds then reset the game

}
