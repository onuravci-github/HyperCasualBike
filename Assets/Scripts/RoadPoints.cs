using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class RoadPoints : MonoBehaviour
{
    public Vector3[] createPoints;
    private int pointNumb = 0;
    public GameObject referanceObj;

    public static Vector3[] roadPoints;

    void Awake() { 
        roadPoints = new Vector3[createPoints.Length];
        roadPoints = createPoints;
    }

    // ------------------------------------------------------
    // Editor Road Point Create
    public void PointCreate() {
        Vector3[] tempVector = createPoints;
        createPoints = new Vector3[createPoints.Length + 1];
        for(int i = 0; i < tempVector.Length; i++) {
            createPoints[i] = tempVector[i];
        }
        createPoints[pointNumb] = referanceObj.transform.position;
        pointNumb++;
    }

    [CustomEditor(typeof(RoadPoints))]
    class DecalMeshHelperEditor : Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            RoadPoints myScript = (RoadPoints)target;

            if(GUILayout.Button("Create Point")) {
                myScript.PointCreate();
            }
        }
    }
}
