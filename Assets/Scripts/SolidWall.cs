using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Bike" && FinishPlane.isRaceCompleted) {

            Bikes_AI bike = collision.gameObject.GetComponent<Bikes_AI>();
            bike.animator.enabled = true;
            bike.BombParticle();
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Animator>().SetBool("Game Over", true);
            collision.gameObject.GetComponent<Collider>().enabled = false;
            bike.manAnim.gameObject.AddComponent<Rigidbody>();
            bike.manAnim.gameObject.AddComponent<BoxCollider>();
            bike.manAnim.enabled = false;
            bike.boomImg.SetActive(true);
            Destroy(collision.gameObject, 1f);
        }
        else if(collision.gameObject.tag == "Player" && FinishPlane.isRaceCompleted) {
            Biker_Movement bike = collision.gameObject.GetComponent<Biker_Movement>();
            bike.animator.enabled = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponentInChildren<Animator>().SetBool("Game Over", true);
            collision.gameObject.GetComponent<Collider>().enabled = false;

            bike.manAnim.gameObject.AddComponent<Rigidbody>();
            bike.manAnim.gameObject.AddComponent<BoxCollider>();
            bike.manAnim.enabled = false;
            Destroy(collision.gameObject, 1f);
        }
    }
}
