using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    void Start() {
        GetComponent<PlayableDirector>().stopped += OnCutsceneEnd;
        GetComponent<PlayableDirector>().played += OnCutsceneStart;
    }
    void OnTriggerEnter(Collider c) {
        GetComponent<PlayableDirector>().Play();
    }
    void OnCutsceneStart(PlayableDirector director) {
        // disable player movement and camera
        playerCamera.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    void OnCutsceneEnd(PlayableDirector director) {
        //reactivate player controls and camera
        playerCamera.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
