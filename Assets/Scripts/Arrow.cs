using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float duration;

    [SerializeField] private int damage = 2;

    private Vector3 shootDir;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        duration = Timer(duration);

        if (duration < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        IDamage hit = collision.gameObject.GetComponent<IDamage>();

        if (hit != null)
        {
            hit.Damage(damage);
            Destroy(this.gameObject);
        }

        Debug.Log(hit);
        /*
        if (collision.gameObject.tag == "Animal")
        {
        }
        */
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
