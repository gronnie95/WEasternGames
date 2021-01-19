using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSounds : MonoBehaviour
{
    public AudioClip lightAttackSound;
    public GameObject diageticSoundManagerGameObject;
    private DiageticSoundManager diageticSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        diageticSoundManager = diageticSoundManagerGameObject.GetComponent<DiageticSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimation_IsLightAttackActive()
    {
        diageticSoundManager.Add3DSound(lightAttackSound,gameObject.transform.position); 
        Debug.Log("Light Attack Sound");
    }
}
