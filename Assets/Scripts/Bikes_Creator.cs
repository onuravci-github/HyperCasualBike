using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bikes_Creator : MonoBehaviour
{
    private IEnumerator createCroutine;
    public GameObject bike;

    public int maxBikeNumb;
    private int countBike = 1;
    private int exceedPoint = 0;
    private float createTime = 0.25f;
    private int createCount = 0;
    private float deviation;

    public static bool gameStart;

    private int[] flagNumbs = new int[] {0,1,2,3,4,5,6,7,8,9,10,11};
    
    void Start() {
        gameStart = false;
        FlagNumbRandom();
        createCroutine = CreateBikes(createTime);
        StartCoroutine(createCroutine);
    }

    public void FlagNumbRandom() {
        for(int i = 0; i < 10; i++) {
            int changeNumb1 = Random.Range(0, 11);
            int changeNumb2 = Random.Range(0, 11);
            int tempNumb = flagNumbs[changeNumb1];
            flagNumbs[changeNumb1] = flagNumbs[changeNumb2];
            flagNumbs[changeNumb2] = tempNumb;
        }
    }
    private IEnumerator CreateBikes(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if(createCount < maxBikeNumb) {
            deviation = Random.Range(-3f, 3f);
            var bikeObj = Instantiate(bike, new Vector3(RoadPoints.roadPoints[exceedPoint].x + deviation, 0, RoadPoints.roadPoints[exceedPoint].z + (countBike + 1) * 12), Quaternion.identity, transform);
            bikeObj.GetComponent<Bikes_AI>().flagNumb = flagNumbs[createCount];
            
            countBike++;
            createCount++;
            Crowns.bikeTransforms[createCount] = bikeObj.transform;
            if(RoadPoints.roadPoints[exceedPoint].z + (countBike + 1) * 12 > RoadPoints.roadPoints[exceedPoint + 1].z) {
                exceedPoint++;
                countBike = 0;
            }
            createCroutine = CreateBikes(createTime);
            StartCoroutine(createCroutine);
        }
        else {
            Crowns.bikeTransforms[0] = GameObject.FindGameObjectWithTag("Player").transform;
            FinishPlane.startTime = Time.time;
            gameStart = true;
        }
    }
}
