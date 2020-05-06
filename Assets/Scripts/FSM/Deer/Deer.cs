using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum States
{
    Idle,
    Wander,
    Run,
    Die
}

public class Deer : MonoBehaviour, IDamage
{
    //public
    public int healthPoints;
    public float walkSpeed;
    public float runSpeed;
    public float deathTimer;
    public Transform[] wanderPoints;
    public States startState;
    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    [SerializeField] private int amount;
    [SerializeField] private Items item;
    [SerializeField] private GameObject deerObject;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject dieParticle;

    private int currentHealth;
    private float currentDeathTime;
    private bool dead = false;
    private Animator anim;
    private BoxCollider collision;
    private AudioSource audioSource;
    private void Awake()
    {
        blackBoard.wanderPoints = wanderPoints;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();
        blackBoard.deer = this;

        anim = GetComponentInChildren<Animator>();
        collision = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();

        ResetDeer();
        SetAnimation("isWalking", false);

        fsm = new FSM(blackBoard, startState, 
            new IdleState(States.Idle), 
            new WanderState(States.Wander), 
            new RunState(States.Run), 
            new DieState(States.Die));
    }

    private void Update()
    {
        if (dead)
        {
            currentDeathTime = Timer(currentDeathTime);

            if(currentDeathTime < 0)
            {
                ResetDeer();
            }
        }
        else
        {
            if (fsm != null)
            {
                fsm.OnUpdate();
            }
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
        currentHealth -= damage;
        Instantiate(bloodParticle, transform.position, transform.rotation);
        SetAnimation("isWalking", false);
        audioSource.Play();
        fsm.SwitchState(States.Run);
        Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ItemCollections.Instance.IncreaseHandItem(item, amount);
        collision.enabled = false;
        anim.SetTrigger("die");
        dead = true;
    }

    private void ResetDeer()
    {
        currentDeathTime = deathTimer;
        currentHealth = healthPoints;
        dead = false;
        collision.enabled = true;
        deerObject.SetActive(true);
        SetAnimation("isWalking", true);
    }

    public void DisabeleObject()
    {
        deerObject.SetActive(false);
        Instantiate(dieParticle, transform.position, dieParticle.transform.rotation);
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}