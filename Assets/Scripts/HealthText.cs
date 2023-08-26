using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 250;
    public Vector3 floatDirection = new Vector3(0, 1, 0);
    public TextMeshProUGUI textMesh;
    

    RectTransform rTransform;
    Color startingColor;

    float timeElasped = 0.0f;

    void Start(){
        rTransform = GetComponent<RectTransform>();
        startingColor = textMesh.color;
    }

    void Update(){
        timeElasped += Time.deltaTime;

        rTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElasped / timeToLive));

        if (timeElasped > timeToLive){
            Destroy(gameObject);
        }
    }
}
