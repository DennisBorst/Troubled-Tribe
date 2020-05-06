using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int amount;
    [SerializeField] private float treeGrowTimer;
    [Space]
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject trunk;
    [SerializeField] private Items item;
    [SerializeField] private ParticleSystem chopParticle;
    [SerializeField] private GameObject disappearParticle;

    [SerializeField] private AudioSource audioSource;

    private int currentHealth;
    private float currentGrowTime;
    private bool treeAlive;
    private void Start()
    {
        currentGrowTime = treeGrowTimer;
        currentHealth = health;
        treeAlive = true;

        tree.SetActive(true);
        trunk.SetActive(false);
    }

    private void Update()
    {
        if(treeAlive) { return; }

        GrowTree();
    }

    public void TakeDamage()
    {
        if(!treeAlive) { return; }

        currentHealth--;
        chopParticle.Play();
        audioSource.Play();

        if (currentHealth <= 0)
        {
            treeAlive = false;
            ItemCollections.Instance.IncreaseHandItem(item, amount);
            Instantiate(disappearParticle, transform.position, disappearParticle.transform.rotation);
            trunk.SetActive(true);
            tree.SetActive(false);
        }
    }

    private void GrowTree()
    {
        currentGrowTime = Timer(currentGrowTime);

        if (currentGrowTime <= 0)
        {
            currentGrowTime = treeGrowTimer;
            currentHealth = health;
            treeAlive = true;

            tree.SetActive(true);
            trunk.SetActive(false);
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
