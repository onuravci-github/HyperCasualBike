using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    // 0:Click | 1:Buy | 2:Sell | 3:Pop-1 | 4:Pop-2 | 5:Winx | 6:Fairy
    public AudioSource[] sfx;
    public static AudioSource[] sfxs;

    void Start() {
        sfxs = new AudioSource[sfx.Length];
        sfxs = sfx;
    }
    public void SFXStart(int numb)
    {
        
            if (!sfx[numb].isPlaying){
                sfx[numb].Play();
            }
            else{
                sfx[numb].Stop();
                sfx[numb].Play();
            }
    }

    public static void SFX_Start(int numb) {

        if(!sfxs[numb].isPlaying) {
            sfxs[numb].Play();
        }
        else {
            sfxs[numb].Stop();
            sfxs[numb].Play();
        }
    }

}
