using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxDetection : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
     {
         transform.parent.GetComponent<BasicEnemy>().CollisionDetected(this);
     }
}
