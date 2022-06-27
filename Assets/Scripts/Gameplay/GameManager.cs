using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float _respawnTimeAmount = 0;



    //The respawn timer
    IEnumerator RespawnTime(float ResetTime)
    {
        _respawnTimeAmount = ResetTime;
        yield return new WaitForSeconds(ResetTime);

    }


    public void Reset()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
