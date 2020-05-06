using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private Items item;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ItemCollections.Instance.IncreaseHandItem(item, amount);
            Destroy(this.gameObject);
        }
    }
}
