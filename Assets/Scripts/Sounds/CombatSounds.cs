using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSounds : MonoBehaviour
{
    public AudioClip[] lightAttackSwingSounds;
    public AudioClip[] swordBlockSounds;
    public AudioClip[] perfectBlockSounds;
    public AudioClip[] heavyAttackSwingSounds;
    public AudioClip[] swordBodySlashSounds;
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
        // pick a random sound from the array
        AudioClip randomSwingSound = RandomClip(lightAttackSwingSounds);

        diageticSoundManager.Add3DSound(randomSwingSound); 

        //Debug.Log("Light Attack Sound");
    }

    public void OnAnimation_isGetCriticalHit()
    {
        // pick a random sound from the array
        AudioClip randomSwingSound = RandomClip(swordBodySlashSounds);

        diageticSoundManager.Add3DSound(randomSwingSound); 

        //Debug.Log("Light Attack Sound");
    }

    public void OnAnimation_IsHeavyAttackActive()
    {
        // pick a random sound from the array
        AudioClip randomSwingSound = RandomClip(heavyAttackSwingSounds);

        diageticSoundManager.Add3DSound(randomSwingSound); 
    }

    public void OnAnimation_BlockingImpact() {
        // pick a random sound from the array
        AudioClip randomSwingSound = RandomClip(swordBlockSounds);

        diageticSoundManager.Add3DSound(randomSwingSound); 
    }

    // impact from perfect block animation start event
     public void OnAnimation_StopAttackCollision() {
        // pick a random sound from the array
        AudioClip randomSwingSound = RandomClip(perfectBlockSounds);

        diageticSoundManager.Add3DSound(randomSwingSound); 
    }

    // helper method to choose a random clip from an array
    private AudioClip RandomClip(AudioClip[] clips) {
        System.Random random = new System.Random();

        int randomIndex;

        if (clips.Length > 1) {
            randomIndex = random.Next(0, clips.Length);
        } else {
            randomIndex = 0;
        }
        
        return clips[randomIndex];
    }
}
