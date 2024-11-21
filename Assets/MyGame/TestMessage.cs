using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessage : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(gameObject.name + "与碰撞" + collisionInfo.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "与触发" + other.gameObject.name);
    }
}
