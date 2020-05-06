using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private Items item;
    [Space]
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask checkForLayerMask;
    [SerializeField] private Transform raycastTransform;
    [SerializeField] private Transform particlePoint;
    [SerializeField] private GameObject particle;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckWater();
        }
    }

    private void CheckWater()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastTransform.position, raycastTransform.forward, out hit, raycastDistance, checkForLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            audioSource.Play();
            Instantiate(particle, particlePoint.position, particlePoint.rotation);
            ItemCollections.Instance.IncreaseHandItem(item, amount);
        }
    }
}
