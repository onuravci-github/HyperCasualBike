using UnityEngine;
using UnityEngine.UI;

public class SfxButton : MonoBehaviour
{
    public Sprite[] sprite;
    public static bool isSfxOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SfxOnOff(){
        if(!isSfxOn) {
            isSfxOn = true;
            this.GetComponent<Image>().sprite = sprite[0];
            
        }
        else{
            isSfxOn = false;
            this.GetComponent<Image>().sprite = sprite[1];
        }
    }
    
    private void OnEnable() {
        if(!isSfxOn) {
            isSfxOn = true;
            this.GetComponent<Image>().sprite = sprite[0];

        }
        else {
            isSfxOn = false;
            this.GetComponent<Image>().sprite = sprite[1];
        }
    }
}
