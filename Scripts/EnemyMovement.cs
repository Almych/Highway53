
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    public Transform[] points;
    public string[] animStates;
    public float startTime, rotationSpeed;
    private float waitTime;
    public GameObject root, root2;
    public float attackRange = 3f;
    public LayerMask player, obstruction;
    private int randomPoints;
    [SerializeField] public GameObject target, head;
     private AnimChangeEnemy anim;
    [Range(0, 360)]
    public float angle;
    public float radius;
    [SerializeField] private InteractionAnimation interact;
    public bool canSee, sawPlayer, inAttackVision, isAttack;
    [SerializeField] private NavMeshAgent _agent;
    internal Vector3 lastPos;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip attack;
    


    private void Start()
    {
        anim = GetComponent<AnimChangeEnemy>();
         waitTime = startTime;
         randomPoints = Random.Range(0, points.Length);
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        inAttackVision = Physics.CheckSphere(transform.position, attackRange, player);
        
        
             Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, player);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                    if (!Physics.Raycast(transform.forward, directionToTarget, distanceToTarget, obstruction))
                    {

                        
                        canSee = true;
                        sawPlayer = true;
                        lastPos = target.transform.position;
                    }
                    else
                        canSee = false;
                      
                    
                
            }
            else
                canSee = false;
              


        }
        else if (canSee)
            canSee = false;
            
        


        if (inAttackVision)
        {
           
            AttackEnemy();
        }
        if (!canSee && !sawPlayer && !interact.dropped && !inAttackVision) Patrolling();
        else if (canSee && sawPlayer && !inAttackVision)
        {
            Chase();
        }
        else if (!canSee && sawPlayer && !inAttackVision) {
            Check();
        } 
        if (interact.dropped && !canSee && !sawPlayer && !inAttackVision) DroppedCheck();

        if (transform.position.y >= 1 && target.transform.position.y < 0 || transform.position.y < 0 && target.transform.position.y >= 1)
        {
            root.SetActive(true);
        }
        else root.SetActive(false);
        if (transform.position.y <= -2 && target.transform.position.y >= -2 || transform.position.y >= -2 && target.transform.position.y <= -2)
        {
            root2.SetActive(true);
        }
        else root2.SetActive(false);

       
       
    }

    void Patrolling()
    {
        
        _agent.speed = 4;
       
        _agent.SetDestination(points[randomPoints].position);

        if (Vector3.Distance(transform.position, points[randomPoints].position) <= 0.2f)
        {

            if (waitTime <= 0)
            {
                anim.EnemyAnimationChange(animStates[1]);
                waitTime = startTime;
                randomPoints = Random.Range(0, points.Length);
            }
            else
            {
                waitTime -= Time.deltaTime;
                anim.EnemyAnimationChange(animStates[0]);
            }

        }
        else
        {
            anim.EnemyAnimationChange(animStates[1]);
            _agent.SetDestination(points[randomPoints].position);
        }

    }

    void Chase()
    {
        _agent.SetDestination(target.transform.position);
        anim.EnemyAnimationChange(animStates[2]);
        _agent.speed = 7;
       
    }

    void Check()
    {
        _agent.speed = 7;
       _agent.SetDestination(lastPos);
        if (Vector3.Distance(transform.position, lastPos) <= 3.2f)
        {
          
            if (waitTime <= 0)
            {
                anim.EnemyAnimationChange(animStates[2]);
                waitTime = startTime;
                sawPlayer = false;
            }
            else
            {
                anim.EnemyAnimationChange(animStates[0]);
                waitTime -= Time.deltaTime;
            }
               
        }
        else
        {
            _agent.SetDestination(lastPos);
            anim.EnemyAnimationChange(animStates[2]);
        }
            
            
        
    }

    
    void DroppedCheck ()
    {
        _agent.speed = 7;
        _agent.SetDestination(interact.droppedTrigger.position);
        if (Vector3.Distance(transform.position, interact.droppedTrigger.position) <= 3.2f)
        {
            anim.EnemyAnimationChange(animStates[0]);
            if (waitTime <= 0)
            {
                waitTime = startTime;
                interact.dropped = false;
            }
            else
                waitTime -= Time.deltaTime;
        }
        else
        {
            _agent.SetDestination(interact.droppedTrigger.position);
            anim.EnemyAnimationChange(animStates[2]);
        }
        }

    
    IEnumerator CoolDown ()
    {
        target.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(1.3f);
        source.Stop();
        interact.DieAnim();
    }
    
    private void AttackEnemy ()
    {
        
        sawPlayer = false;
        canSee = false;
        if (!isAttack)
        {
            source.PlayOneShot(attack);
            transform.GetComponent<Collider>().enabled = false;
            target.transform.LookAt(head.transform.position);
            transform.LookAt(target.transform.position);
            target.transform.GetChild(0).GetComponent<CameraController>().enabled = false;
        }
        isAttack = true;
        _agent.SetDestination(transform.position);
        anim.EnemyAnimationChange(animStates[3]);
        StartCoroutine(CoolDown());
    }
    
    
}
