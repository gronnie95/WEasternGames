using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Overseer : MonoBehaviour
{
    public GameObject player;
    public GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerStats>().death += OnPlayerDeath;
    }

    void OnPlayerDeath() {
        // play some animation
        director.GetComponent<PlayableDirector>().Play();
    }
}
