using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 10f;
    public Transform player;

    void LateUpdate(){
        Vector3 newPosition = player.position;
        newPosition.z = -10;
        newPosition.y = 6.24f;

        
        transform.position = Vector3.Slerp(transform.position, newPosition, speed * Time.deltaTime);
    }
}
