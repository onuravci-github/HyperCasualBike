using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Biker_Movement : MonoBehaviour
{
    public Rigidbody bikerRigid;
    public Transform bikeHandle;
    public Transform bikeFrame;
    public Transform[] bikeTire;
    public Animator animator;
    public Animator manAnim;

    private float downPosition;
    private float oldRotateY;
    private float speedXCoord;
    private float speedBike = 500f;
    public float speed = 35f;
    private float speedUp = 1;
    private bool directionControl = true;
    public bool isRaceCompleted = false;

    public int flagNumb;
    public bool isBot = false;

    private bool mouseDownActive = false;
    private bool mouseOverActive = false;

    public GameObject shotbullet;
    public GameObject gainBulletBar;
    public GameObject speedParticle;
    public GameObject[] bullets;
    public int bulletValue = 0;
    public Text bulletText;
    // Start is called before the first frame update
    void Start() {
        animator = this.GetComponentInChildren<Animator>();
        animator.enabled = false;
        bikerRigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if(Bikes_Creator.gameStart && !isRaceCompleted) {
            BikeMovement();
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                MouseDown();
                mouseDownActive = true;
            }
            else if(Input.GetKey(KeyCode.Mouse0) && mouseDownActive) {
                MouseOver();
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0)) {
                mouseDownActive = false;
            }
        }
    }

    public void BikeMovement() {
        this.transform.eulerAngles = new Vector3(0, Mathf.Clamp(speedXCoord/10, -50,50), 0);
        bikerRigid.velocity = Vector3.Normalize(new Vector3(speedXCoord * Time.deltaTime, 0f, speedBike * Time.deltaTime))* speed* speedUp;
        BikeAnimation();
    }

    public void BikeAnimation() {
        manAnim.enabled = true;
        /*Debug.Log("A = " + oldRotateY);
        Debug.Log("B = " + this.transform.eulerAngles.y);
        if(oldRotateY + 0.5f >= this.transform.eulerAngles.y && oldRotateY -0.5f <= this.transform.eulerAngles.y) {
            Debug.Log("C = " + oldRotateY);
            Debug.Log("D = " + bikeFrame.eulerAngles.y);
            bikeFrame.eulerAngles = 2 * this.transform.eulerAngles;
            bikeHandle.eulerAngles = 2 * this.transform.eulerAngles;
            oldRotateY = this.transform.eulerAngles.y;
        }
        else {
            Debug.Log("C = " + oldRotateY);
            bikeFrame.eulerAngles =  this.transform.eulerAngles;
            bikeHandle.eulerAngles = this.transform.eulerAngles;
            oldRotateY = this.transform.eulerAngles.y;
        }
        */
        bikeTire[0].Rotate(Vector3.right * Time.deltaTime * 180);
        bikeTire[1].Rotate(Vector3.right * Time.deltaTime * 180);
        bikeTire[2].Rotate(Vector3.right * Time.deltaTime * 360);
    }

    private void MouseDown() {
        downPosition = Input.mousePosition.x;
    }

    private void MouseOver() {
        if(downPosition < Input.mousePosition.x) {
            if(!directionControl) {
                downPosition = Input.mousePosition.x;
                directionControl = true;
            }
            speedXCoord = Mathf.SmoothStep(speedXCoord, Input.mousePosition.x - downPosition, Time.deltaTime * 10f);
            directionControl = true;
        }
        else if(downPosition > Input.mousePosition.x) {
            if(directionControl) {
                downPosition = Input.mousePosition.x;
                directionControl = false;
            }
            speedXCoord = Mathf.SmoothStep(speedXCoord, Input.mousePosition.x - downPosition, Time.deltaTime * 10f); 
        }
    }
    public void BulletGain() {
        if(bulletValue < 6) {
            bulletValue++;
            bullets[bulletValue - 1].SetActive(true);
            bulletText.text = "+" + (int.Parse(bulletText.text[1].ToString()) + 1);
        }
        else {
            bulletText.text = "Max";
        }
    }
    private void ShootBullet() {
        speedParticle.SetActive(true);
        speedUp = 1.25f;
        if(bulletValue >= 1 && !isRaceCompleted) {
            var tempBullet = Instantiate(shotbullet, this.transform.position + Vector3.up * 2, Quaternion.identity);
            tempBullet.GetComponent<Rigidbody>().velocity = (this.transform.forward * 125);
            bulletValue--;
            bullets[bulletValue].SetActive(false);
            Invoke("ShootBullet",1f);
        }
        else {
            Invoke("SpeedNormal", 0.25f);
        }
    }
    private void SpeedNormal() {
        speedParticle.SetActive(false);
        speedUp = 1f;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            speedUp = 0.75f;
            gainBulletBar.SetActive(true);
            animator.gameObject.transform.position -= Vector3.up * 0.1f;
            CancelInvoke("ShootBullet");
            speedParticle.SetActive(false);
        }
    }
    void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            speedUp = 1f;
            gainBulletBar.SetActive(false);
            animator.gameObject.transform.position += Vector3.up * 0.1f;
            bulletText.text = "+" + 0;
            if(bulletValue >= 1) {
                Invoke("ShootBullet",0.5f);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Crystal") {
            Destroy(other.gameObject);
            SFX.SFX_Start(0);
        }
    }
    public void CameraFree() {
        GameObject cameraObject = this.GetComponentInChildren<Camera>().gameObject;
        cameraObject.transform.position = new Vector3(0, cameraObject.transform.position.y, cameraObject.transform.position.z);
        cameraObject.transform.eulerAngles = new Vector3(25, 0, 0);
        cameraObject.transform.parent = null;
        
    }
}
