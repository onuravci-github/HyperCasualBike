using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite[] sprite;
    public static bool isSoundOn = true;
    // Start is called before the first frame update
    


    public void SoundOnOff(){
        if(!isSoundOn) {
            isSoundOn = true;
            this.GetComponent<Image>().sprite = sprite[0];
        }
        else{
            isSoundOn = false;
            Debug.Log("2");
            this.GetComponent<Image>().sprite = sprite[1];
        }
    }
    private void OnEnable() {
        if(isSoundOn) {
            isSoundOn = true;
            this.GetComponent<Image>().sprite = sprite[0];
        }
        else {
            Debug.Log("1");
            isSoundOn = false;
            this.GetComponent<Image>().sprite = sprite[1];
        }
    }
}
