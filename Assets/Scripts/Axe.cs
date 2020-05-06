using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask checkForLayerMask;
    [SerializeField] private Transform chopTransform;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckTree();
        }
    }

    private void CheckTree()
    {
        RaycastHit hit;
        if (Physics.Raycast(chopTransform.position, chopTransform.forward, out hit, raycastDistance, checkForLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Tree tree = hit.transform.GetComponent<Tree>();
            tree.TakeDamage();
        }
    }
}
