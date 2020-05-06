using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class NPC : MonoBehaviour, IDamage
{
    //public
    public int healthPoints;
    public float runSpeed;
    public float walkSpeed;
    public Transform[] wanderPoints;
    public States startState;
    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    [SerializeField] private int amount;
    [SerializeField] private Items item;

    [Header("Extra Effects")]
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Material whiteMat;
    private Material[] oldMaterials;

    private Animator anim;
    private AudioSource audioSource;
    private MeshRenderer[] meshRenderers;
    private MeshRenderer[] oldRenderers;

    private void Awake()
    {
        blackBoard.wanderPoints = wanderPoints;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();
        blackBoard.npc = this;

        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        oldRenderers = GetComponentsInChildren<MeshRenderer>();

        fsm = new FSM(blackBoard, startState,
            new NPCIdleState(States.Idle),
            new NPCWanderState(States.Wander),
            new NPCRunState(States.Run));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }
    public void SetAnimation(string state, bool val)
    {
        if (state != null && val != null)
        {
            anim.SetBool(state, val);
        }
    }
    IEnumerator SetanimationBoolFalse(string val, float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.SetBool(val, false);
    }

    public void Damage(int damage)
    {
        Debug.Log("Got hit");
        healthPoints -= damage;
        fsm.SwitchState(States.Run);
        audioSource.Play();
        Instantiate(damageParticle, transform.position, transform.rotation);
        Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);
        /*
        float hitFrames = 30f;
        while(hitFrames > 0)
        {
            hitFrames--;
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = whiteMat;
            }
        }

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = oldMaterials[i];
        }
        */

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ItemCollections.Instance.IncreaseHandItem(item, amount);
        GameManager.Instance.DecreasePeople(this);
        Destroy(this.gameObject);
    }

    public void DieParticle()
    {
        Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);
    }
}
