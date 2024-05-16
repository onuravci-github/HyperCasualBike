using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMusic : MonoBehaviour
{
    public AudioSource[] audioSource;
    private int musicNumb = 0;

    public static GameObject destroyObject;
    void Awake() {
        if(destroyObject == null) {
            destroyObject = transform.gameObject;
            DontDestroyOnLoad(transform.gameObject);
        }
        else if(this.gameObject != destroyObject){
            Destroy(this.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SoundButton.isSoundOn) {
            audioSource[0].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!SoundButton.isSoundOn) {
            audioSource[0].Stop();
        }

        if (!audioSource[musicNumb].isPlaying && SoundButton.isSoundOn)
        {
            /*if (musicNumb == (audioSource.Length - 1))
            {
                musicNumb = 0;
            }
            else
            {
                musicNumb++;
            }*/
            audioSource[0].Play();
        }
    }
}
