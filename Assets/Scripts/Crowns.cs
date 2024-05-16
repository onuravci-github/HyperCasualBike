using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowns : MonoBehaviour
{
    public static Transform[] bikeTransforms = new Transform[11];
    private int[] winnerNumb = new int[11] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public GameObject[] crownsObj;
    private IEnumerator createCroutine;

    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 0;
        bikeTransforms = new Transform[11];
        //createCroutine = WinnerControl(0.1f);
        //StartCoroutine(createCroutine);
    }

    private IEnumerator WinnerControl(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if(bikeTransforms[10] != null) {
            for(int i = 0; i < 11; i++) {
                Debug.Log(i + ". = " + winnerNumb[i]);
            }
        }

        createCroutine = WinnerControl(0.1f);
        StartCoroutine(createCroutine);
    }
    // Update is called once per frame
    void Update() {
        if(Bikes_Creator.gameStart) {
            int c = 0;
            for(int i = 0; i < bikeTransforms.Length; i++) {
                if(bikeTransforms[i] != null) {
                    c++;
                }
            }

            var tempTransforms = new Transform[c];
            c = 0;
            for(int i = 0; i < bikeTransforms.Length; i++) {
                if(bikeTransforms[i] != null) {
                    tempTransforms[c] = bikeTransforms[i];
                    c++;
                }
            }
            bikeTransforms = new Transform[c];
            bikeTransforms = tempTransforms;

            for(int i = 0; i < bikeTransforms.Length; i++) {
                for(int k = i + 1; k < bikeTransforms.Length; k++) {
                    if(bikeTransforms[i].position.z > bikeTransforms[k].position.z) {
                        var temp = bikeTransforms[i];
                        bikeTransforms[i] = bikeTransforms[k];
                        bikeTransforms[k] = temp;
                    }
                }
            }

            int n = tempTransforms.Length - 1;
            if(n >= 2) {
                for(int i = 0; i < 3; i++) {
                    crownsObj[i].transform.position = tempTransforms[n].position + Vector3.up * 5f;
                    n--;
                }
            }
            else {
                for(int i = 0; i < n + 1; i++) {
                    crownsObj[i].transform.position = tempTransforms[n].position + Vector3.up * 5f;
                    n--;
                }
            }

        }
    }

}

