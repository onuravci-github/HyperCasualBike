using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditingMesh : MonoBehaviour
{
    private Mesh planeMesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;
    private int[] triangles;

    private int triangleIndex;
    public float heightChange = -1f;
    private int[] verts = new int[6];

    public GameObject cursorObject;
    public Mesh exampleGround;
    public GameObject particle;

    private float digVolume = 0;
    private bool isActive;

    private Biker_Movement biker_Movement;
    void Start() {
        vertices = new Vector3[exampleGround.vertices.Length];
        triangles = new int[exampleGround.triangles.Length];
        biker_Movement = this.gameObject.GetComponent<Biker_Movement>();
    }
    void Update() {
        if(isActive) {
            MeshEdit();
            GainBulletControl();
        }
    }

    void MeshEdit() {
        Ray ray = Camera.main.ScreenPointToRay(cursorObject.transform.position);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            triangleIndex = hit.triangleIndex;
            if(hit.collider.gameObject.tag == "Ground") {
                planeMesh = hit.transform.gameObject.GetComponent<MeshFilter>().mesh;
                meshCollider = hit.transform.gameObject.GetComponent<MeshCollider>();
                vertices = planeMesh.vertices;
                triangles = planeMesh.triangles;
            }
        }
        else {
            return;
        }
        if(hit.collider.gameObject.tag == "Ground") {
            int triangleDirect = (-2 * (triangleIndex % 2)) + 1;

            for(int i = 0; i < 6; i++) {
                if(i >= 3)
                    verts[i] = (triangleIndex + triangleDirect) * 3 + (i - 3);
                else
                    verts[i] = triangleIndex * 3 + i;
            }

            for(int i = 0; i < 6; i++) {
                vertices[triangles[verts[i]]].y += heightChange*Time.deltaTime;
            }

            MeshUpdate();
        }
    }

    private void GainBulletControl() {
        digVolume += - 20  * heightChange * Time.deltaTime;
        if(digVolume >= 100) {
            digVolume = 0;
            biker_Movement.BulletGain();
            SFX.SFX_Start(1);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            isActive = true;
            particle.SetActive(true);
        }
    }
    void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            isActive = false;
            particle.SetActive(false);
        }
    }

    private void MeshUpdate() {
        planeMesh.Clear();
        planeMesh.vertices = vertices;
        planeMesh.triangles = triangles;
        planeMesh.RecalculateNormals();
        //meshCollider.sharedMesh = planeMesh;
    }
}
