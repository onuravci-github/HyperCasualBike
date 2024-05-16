using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bikes_AI : MonoBehaviour
{
    private Rigidbody bikerRigid;
    private List<Vector3> roadPoints = new List<Vector3>();
    public Animator animator;
    public Animator manAnim;
    private float deviation;
    private int posNumber = 0;
    private bool isDead;
    public bool isRaceCompleted;

    public Animator bikeAnim;
    public Transform bikeHandle;
    public Transform bikeFrame;
    public Transform[] bikeTire;

    public int flagNumb;
    public bool isBot = true;

    public int speed = 20;
    // Normal Speed : 1 - SpeedUp : 2 - SpeedDown : 1/2 
    private float speedUp = 1;
    private float rotateValue;
    private Vector3 velocityVector;

    public GameObject bombParticle;

    public GameObject boomImg;
    public Image imageFlag;
    public Sprite[] flagSprites;
    public Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        deviation = Random.Range(-4f, 4f);
        //roadPoints = new Vector3[RoadPoints.roadPoints.Length];
        roadPoints.AddRange(RoadPoints.roadPoints);
        bikerRigid = this.GetComponent<Rigidbody>();

        imageFlag.sprite = flagSprites[flagNumb];
        nameText.text = "Bot " + flagNumb;
    }

    public void FlagUpdate() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Bikes_Creator.gameStart && !isDead && !isRaceCompleted) {
            BikeMovement();
        }
    }
    public void BikeMovement() {
        if(this.transform.position.z + 10 > roadPoints[posNumber + 1].z && posNumber + 2 <= roadPoints.Count) {
            velocityVector = (roadPoints[posNumber + 2] + roadPoints[posNumber + 1]) / 2 - roadPoints[posNumber] ;
            bikerRigid.velocity = Vector3.Normalize(velocityVector) * speed * speedUp;

            rotateValue = Mathf.Atan2(bikerRigid.velocity.x, bikerRigid.velocity.z) * Mathf.Rad2Deg;
            if(rotateValue < 90) {
                this.transform.eulerAngles = new Vector3(0, rotateValue, 0);
            }
            else {
                this.transform.eulerAngles = new Vector3(0, 90 - rotateValue, 0);
            }
            
        }
        else if(posNumber + 1 <= roadPoints.Count) {
            velocityVector = roadPoints[posNumber + 1] - roadPoints[posNumber] ;
            bikerRigid.velocity = Vector3.Normalize(velocityVector) * speed * speedUp;
            
            rotateValue = Mathf.Atan2(bikerRigid.velocity.x, bikerRigid.velocity.z) * Mathf.Rad2Deg;
            if(rotateValue < 90) {
                this.transform.eulerAngles = new Vector3(0, rotateValue, 0);
            }
            else {
                this.transform.eulerAngles = new Vector3(0, 90 - rotateValue, 0);
            }
        }
        else {
            velocityVector = roadPoints[posNumber] - roadPoints[posNumber - 1] ;
            bikerRigid.velocity = Vector3.Normalize(velocityVector) * speed * speedUp;

            rotateValue = Mathf.Atan2(bikerRigid.velocity.x, bikerRigid.velocity.z) * Mathf.Rad2Deg;
            if(rotateValue < 90) {
                this.transform.eulerAngles = new Vector3(0, rotateValue, 0);
            }
            else {
                this.transform.eulerAngles = new Vector3(0, 90 - rotateValue, 0);
            }
        }
        if(this.transform.position.z > roadPoints[posNumber + 1].z) {
            posNumber++;
        }
        BikeAnimation();
        if(this.transform.position.z > roadPoints[posNumber + 1].z) {
            posNumber++;
        }
    }

    public void BikeAnimation() {
        manAnim.enabled = true;
        //bikeFrame.eulerAngles = 2 * this.transform.eulerAngles;
        //bikeHandle.eulerAngles = 2 * this.transform.eulerAngles;
        bikeTire[0].Rotate(Vector3.right * Time.deltaTime * 180);
        bikeTire[1].Rotate(Vector3.right * Time.deltaTime * 180);
        bikeTire[2].Rotate(Vector3.right * Time.deltaTime * 360);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Soil Bullet" && !isRaceCompleted) {
            bikeAnim.enabled = true;
            bikeAnim.SetBool("Game Over", true);
            isDead = true;
            bikerRigid.velocity = bikerRigid.velocity / 5f;
            this.GetComponent<Collider>().enabled = false;
            boomImg.SetActive(true);
            manAnim.gameObject.AddComponent<Rigidbody>();
            manAnim.gameObject.AddComponent<BoxCollider>();
            manAnim.enabled = false;
            Destroy(this.gameObject, 1.25f);
            BombParticle();
            Destroy(other.gameObject);
            //Time.timeScale = 0.5f;
            //Invoke("TimeNormal", 0.25f);
        }
    }

    public void TimeNormal() {
        Time.timeScale = 1;
    }
    public void BombParticle() {
        Instantiate(bombParticle, this.transform.position, Quaternion.identity, transform);
    } 


}
