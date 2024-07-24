using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Multi_Bullet : MonoBehaviourPunCallbacks
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
        Debug.Log("Carrot Init");
        time = 0;
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        time += Time.deltaTime;
        if (!isCollisioin)
        {
            cachedTransform.position += transform.forward * speed * Time.deltaTime;
            if (time >= 1f && rigidbody.useGravity == false)
                rigidbody.useGravity = true;
        }
        else
        {
            if (time >= 3f)
            {
                if (GameManager.Instance.gamePlayType == GamePlayerType.Multi)
                {
                    Multi_InGameManager.PHObjectPooling.PoolDestroy(gameObject);
                }
                else if (GameManager.Instance.gamePlayType == GamePlayerType.Solo)
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
        }
        if (rigidbody.velocity.x < (maxVelocityX * -1))
        {
            vector3.x = (maxVelocityX * -1);
        }

        if (rigidbody.velocity.y > maxVelocityY)
        {
            vector3.y = maxVelocityY;
        }
        if (rigidbody.velocity.y < (maxVelocityY * -1))
        {
            vector3.y = (maxVelocityY * -1);
        }

        if (rigidbody.velocity.z > maxVelocityZ)
        {
            vector3.z = maxVelocityZ;
        }
        if (rigidbody.velocity.z < (maxVelocityZ * -1))
        {
            vector3.z = (maxVelocityZ * -1);
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
