using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{
    NavMeshAgent navAgent;
    Vector3 destination;
    [SerializeField]AnimalNeedsScriptable animalNeeds;
    
    void Start()
    {
        Init();
    }

    void Init(){
        navAgent = GetComponent<NavMeshAgent>();
        if(navAgent == null)
            Debug.LogError("NavmeshAgent not present on the player");
    }

    public void MoveTowards(Vector3 des, float speed){
        navAgent.isStopped = false;
        navAgent.speed = speed;

        navAgent.SetDestination(des);
    }


    public void SetDestination(Vector3 des){
        destination = des;
        // print(destination);
    }

    public Vector3 GetDestination(){
        return destination;
    }

    public bool OnPath(){
        return navAgent.hasPath;
    }

    public void SetAutoBraking(bool status) => navAgent.autoBraking = status;

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, animalNeeds.viewRadius);  
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(destination, 2f);
    }

}
