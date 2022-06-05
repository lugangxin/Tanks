using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : MonoBehaviour
{
    public LayerMask m_tankMask;
    private static Vector3[] points ={
        new Vector3(-29.8f,0,10.1f),new Vector3(-21.8f,0,8.4f),new Vector3(-23.5f,0,-0.5f),
        new Vector3(-14.3f,0,1.8f),new Vector3(-26f,0,16.8f),new Vector3(-13.5f,0,16.2f)
    };
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isPatrol = false;
    private bool gameover;
    private Vector3 target;
    public Rigidbody m_Shell;
    private Transform m_FireTransform;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_FireTransform = this.transform.Find("FireTransform");
        StartCoroutine(shoot());
    }
    private IEnumerator shoot()
    {
        //for (float i = 1; i > 0; i -= Time.deltaTime)
        //{
        //    yield return 0;
        //}
        while (true) {
            if (Vector3.Distance(transform.position, target) < 10)
            {
                Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
                shellInstance.position = m_FireTransform.position;
                shellInstance.transform.forward = transform.forward;
                shellInstance.AddForce(shellInstance.transform.forward * 20, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(1);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 10,m_tankMask);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                isPatrol = false;
                agent.autoBraking = true;
                target = colliders[i].transform.position;
                agent.SetDestination(colliders[i].transform.position);
            }
        }
        else {
            patrol();
        }
        
    }
    private void patrol() {
        if (isPatrol)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                goToNextPoint();
        }
        else
        {
            agent.autoBraking = false;
            goToNextPoint();
        }
        isPatrol = true;
    }
    private void goToNextPoint() {
        agent.SetDestination(points[destPoint]);
        destPoint = (destPoint + 1) % points.Length;
    }
}
