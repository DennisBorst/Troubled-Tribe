using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpItem : MonoBehaviour
{
    public Items dumpItem;
    [SerializeField] private ParticleSystem particleDeposit;

    private bool colliding;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!colliding) { return; }

        PlaceItems();
    }

    private void PlaceItems()
    {
        
        bool canPlaceItem = false;
        switch (dumpItem)
        {
            case Items.Nothing:
                break;
            case Items.Wood:
                canPlaceItem = ItemCollections.Instance.hand.Wood.Current > 0;
                break;
            case Items.Food:
                canPlaceItem = ItemCollections.Instance.hand.Food.Current > 0;
                break;
            case Items.Water:
                canPlaceItem = ItemCollections.Instance.hand.Water.Current > 0;
                break;
            default:
                break;
        }

        if (canPlaceItem) 
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                particleDeposit.Play();
                audioSource.Play();
                ItemCollections.Instance.TransferHandItems(dumpItem);
            }
        }
    }

    #region Collision
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            colliding = false;
        }
    }
    #endregion
}
