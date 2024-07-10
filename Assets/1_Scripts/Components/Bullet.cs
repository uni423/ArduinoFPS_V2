using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float maxVelocityX, maxVelocityY, maxVelocityZ;

    Transform cachedTransform;
    Rigidbody rigidbody;
    bool isCollisioin;

    private float time = 0;

    void Start()
    {
        cachedTransform = this.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        isCollisioin = false;
        time = 0;
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (!isCollisioin)
        {
            cachedTransform.position += transform.forward * speed * Time.deltaTime;
            if (time >= 1f && rigidbody.useGravity == false)
                rigidbody.useGravity = true;

            //rigidbody.AddForce(cachedTransform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            //limitSpeed();
        }
        else
        {
            if (time >= 3f)
            {
                InGameManager.ObjectPooling.Despawn(gameObject);
            }
        }
    }

    void limitSpeed()
    {
        Vector3 vector3 = rigidbody.velocity;

        if (rigidbody.velocity.x > maxVelocityX)
        {
            vector3.x = maxVelocityX;
            //rigidbody.velocity = new Vector3(maxVelocityX, rigidbody.velocity.y, rigidbody.velocity.z);
        }
        if (rigidbody.velocity.x < (maxVelocityX * -1))
        {
            vector3.x = (maxVelocityX * -1);
            //rigidbody.velocity = new Vector3((maxVelocityX * -1), rigidbody.velocity.y, rigidbody.velocity.z);
        }

        if (rigidbody.velocity.y > maxVelocityY)
        {
            vector3.y = maxVelocityY;
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, maxVelocityY, rigidbody.velocity.z);
        }
        if (rigidbody.velocity.y < (maxVelocityY * -1))
        {
            vector3.y = (maxVelocityY * -1);
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, (maxVelocityY * -1), rigidbody.velocity.z);
        }

        if (rigidbody.velocity.z > maxVelocityZ)
        {
            vector3.z = maxVelocityZ;
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, maxVelocityZ);
        }
        if (rigidbody.velocity.z < (maxVelocityZ * -1))
        {
            vector3.z = (maxVelocityZ * -1);
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, (maxVelocityZ * -1));
        }
        rigidbody.velocity = vector3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        time = 0f;
        if (collision.gameObject.CompareTag("Enemy") && !isCollisioin)
        {
            RabbitUnit rabbitUnit = (RabbitUnit)collision.transform.GetComponent<RabbitUnitObject>().unit;
            rabbitUnit.Hit(AttackType.Normal);
        }
        
        isCollisioin = true;
    }
}
