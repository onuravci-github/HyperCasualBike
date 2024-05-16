using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPlane : MonoBehaviour
{
    // Arr[0] : First - Arr[1] : Second - Arr[2] : Thirst 
    public Transform[] standing;
    public Transform wall;
    private GameObject[] bikeObjects = new GameObject[3];
    public Sprite[] flagSprites;
    public Image[] winFlags ;
    public Text[] winText ;
    private float[] times = new float[3];
    private string[] winnerNumb = new string[] { "1st ", "2nd ", "3rd " };

    // threeFinishers value == 3 : Race Completed
    public int threeFinishers = 0;
    public int countBike = 0;
    private bool isFinish;
    public static bool isRaceCompleted;
    public static float startTime;

    public GameObject confetti;
    // Start is called before the first frame update
    private void Start()
    {
        startTime = 0;
        isRaceCompleted = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isFinish) {
            for(int i = countBike; i < threeFinishers; i++) {
                if(bikeObjects[i].transform.position.z > standing[i].position.z) {
                    bikeObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    bikeObjects[i].transform.Rotate(0, 180, 0);
                    bikeObjects[i].GetComponentInChildren<Canvas>().transform.Rotate(0, 180, 0);
                    bikeObjects[i].GetComponentInChildren<Animator>().enabled = false;
                    if(bikeObjects[i].GetComponent<Biker_Movement>() != null) {
                        bikeObjects[i].GetComponent<Biker_Movement>().manAnim.enabled = false;
                    }
                    else {
                        bikeObjects[i].GetComponent<Bikes_AI>().manAnim.enabled = false;
                    }

                    Instantiate(confetti, bikeObjects[i].transform.position, Quaternion.identity);
                    countBike++;
                    if(i == 2) {
                        isFinish = true;
                    }
                }
                else {
                    bikeObjects[i].GetComponent<Rigidbody>().velocity = Vector3.Normalize(standing[i].position - bikeObjects[i].transform.position) * 25;
                }
            }
        }
        
        
    }

    private void WallUp() {
        if(wall.localScale.y <= 1) {
            wall.localScale = Vector3.up * 0.075f + wall.localScale;
            Invoke("WallUp", 0.05f);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if((other.gameObject.tag == "Player" || other.gameObject.tag == "Bike") && threeFinishers < 3) {
            threeFinishers++;
            if(threeFinishers == 3) {
                isRaceCompleted = true;
                WallUp();
            }
            if(other.gameObject.tag == "Bike") {
                other.gameObject.GetComponent<Bikes_AI>().isRaceCompleted = true;
                other.gameObject.GetComponent<Collider>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Bike_AI_WinPanel(other.gameObject);
            }
            else if(other.gameObject.tag == "Player") {
                other.gameObject.GetComponent<Biker_Movement>().isRaceCompleted = true;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                other.gameObject.GetComponent<Collider>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Biker_Movement>().CameraFree();
                Bike_WinPanel(other.gameObject);
                
            }
            bikeObjects[threeFinishers - 1] = other.gameObject;

        }
        else if(isRaceCompleted) {
            if(other.gameObject.tag == "Bike") {
                other.gameObject.GetComponent<Bikes_AI>().isRaceCompleted = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * other.gameObject.GetComponent<Bikes_AI>().speed * 0.5f;
            }

            else if(other.gameObject.tag == "Player") {
                other.gameObject.GetComponent<Biker_Movement>().isRaceCompleted = true;
                other.gameObject.GetComponent<Collider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * other.gameObject.GetComponent<Biker_Movement>().speed * 0.5f;
                other.GetComponent<Biker_Movement>().CameraFree();
            }
        }
    }

    private void Bike_AI_WinPanel(GameObject bikeObj) {
        int numb = threeFinishers - 1;
        times[numb] = Time.time - startTime;
        winFlags[numb].sprite = flagSprites[bikeObj.GetComponent<Bikes_AI>().flagNumb];

        if(times[numb] >= 60) {
            winText[numb].text = winnerNumb[numb] + (times[numb]/60).ToString("F0") + "." + (times[numb] % 60).ToString("F0");
        }
        else {
            winText[numb].text = winnerNumb[numb] +  "00." + times[numb].ToString("F0");
        }
    }
    private void Bike_WinPanel(GameObject bikeObj) {
        int numb = threeFinishers - 1;
        times[numb] = Time.time - startTime;
        winFlags[numb].sprite = flagSprites[bikeObj.GetComponent<Biker_Movement>().flagNumb];

        if(times[numb] >= 60) {
            winText[numb].text = winnerNumb[numb] + (times[numb] / 60).ToString("F0") + "." + (times[numb] % 60).ToString("F0");
        }
        else {
            winText[numb].text = winnerNumb[numb] + "00." + times[numb].ToString("F0");
        }
    }
}
