using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiageticSoundManager : MonoBehaviour
{
    private LinkedList<GameObject> audioSourceList;
    private Queue<GameObject> finishedAudioSourceQueue;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceList = new LinkedList<GameObject>();
        finishedAudioSourceQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if finished playing then remove from list.. is it destroyed in memory?.. maybe garbage collector handles that

        foreach(GameObject source in audioSourceList) {
            AudioSource audioSource = source.GetComponent<AudioSource>();
            if (!audioSource.isPlaying) { // check if audio is finished playing
                finishedAudioSourceQueue.Enqueue(source); // add to queue for deletion, modification mid iteration is not allowed
            }
        }

        // remove from list, delete sound objects, use the queue

        while (finishedAudioSourceQueue.Count > 0) {
            GameObject finished = finishedAudioSourceQueue.Dequeue(); // dequeue to get reference to finished audio object
            audioSourceList.Remove(finished);  //remove from the list, so won't cause any reference issues
            Destroy(finished); // destroy game object, deleting from list does not destroy game object, still exists in game world
        }
    }

    public void Add3DSound(AudioClip audioClip, Vector3 position) {
        GameObject newSoundObject = new GameObject(); // create a game object, cant directly create a sound source

        newSoundObject.AddComponent<AudioSource>(); // add a sound source component to object

        audioSourceList.AddLast(newSoundObject); // put into the queue

        AudioSource newSource = newSoundObject.GetComponent<AudioSource>(); // get audio source component from created object

        newSource.transform.position = position; // set position of sound

        newSource.spatialBlend = 1.0f; // set 3D Sound

        newSource.clip = audioClip; // assign audio clip

        newSource.Play(); // immediately start playing sound
    }
}
