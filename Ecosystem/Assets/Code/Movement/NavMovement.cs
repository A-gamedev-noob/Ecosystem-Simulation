using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{
    NavMeshAgent navAgent;
    [SerializeField]Vector3 destination, previousDestination;
    public bool usePrevDestination = false;
    float speed;
    [SerializeField]AnimalNeedsScriptable animalNeeds;
    
    void Start()
    {
        Init();
    }

    void Init(){
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = animalNeeds.speed;
        if(navAgent == null)
            Debug.LogError("NavmeshAgent not present on the player");
    }

    public void MoveTowards(){
        if(navAgent.isStopped)

        navAgent.isStopped = false;
        if(usePrevDestination){
            navAgent.SetDestination(previousDestination);
        }else{
            navAgent.SetDestination(destination);
        }


    }

    public void MovetoPreviousPoint(bool condition){
        usePrevDestination = condition;;
    }


    public void SetDestination(Vector3 des){
        previousDestination = destination;
        destination = des;
        // print(destination);
    }

    public Vector3 GetDestination(){
        return destination;
    }

    public bool OnPath(){
        return navAgent.hasPath;
    }

    public Vector3 SetDestinationByDirection(Vector3 dir){

        Vector3 right;
        right = Vector3.Cross(dir, Vector3.up);
        dir = Vector3.Cross(Vector3.up, right);

        float angle = Mathf.Atan2(Vector3.Dot(Vector3.forward, right), Vector3.Dot(Vector3.forward, dir)) * Mathf.Rad2Deg;;

        Vector3 topPoint, pointOnArc;

        if (angle == 0f)
            angle = Random.Range(0, 360);
        angle *= Mathf.Deg2Rad;

        pointOnArc.x = transform.position.x + (animalNeeds.viewRadius * Mathf.Sin(angle));
        pointOnArc.z = transform.position.z + (animalNeeds.viewRadius * Mathf.Cos(angle));
        pointOnArc.y = 100f;
        topPoint = pointOnArc;
        RaycastHit[] hits = Physics.RaycastAll(topPoint, Vector3.down, 200);

        if (hits != null){
            foreach (RaycastHit hit in hits){
                if (hit.collider.CompareTag("Terrain")){
                    pointOnArc = hit.point;
                }
            }
        }

        return pointOnArc;

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, animalNeeds.viewRadius);  
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, 2f);
    }

}
