using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float offset;
    private Vector2 velocity;
    private Transform unitTransform;
    public float smoothTimeX;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        velocity = transform.position;
    }

    public void setUnit(Unit unit){
        unitTransform = unit.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if(unitTransform != null){
            float posX = Mathf.SmoothDamp(transform.position.x, unitTransform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, unitTransform.position.y, ref velocity.y, smoothTimeX);
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
