using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationNode : Node{
    Vector3 directionToMove = Vector3.zero;

    NavMovement navMovement;
    AnimalNeedsScriptable animalNeeds;
    Transform self;
    int timesRun = 0;

    Vector3 topPointG, pointOnArcG;

    public SetDestinationNode(NavMovement navMovement, AnimalNeedsScriptable animalNeeds){
        this.navMovement = navMovement;
        this.animalNeeds = animalNeeds;
        self = navMovement.transform;
    }

    public override NodeState Evaluate(){
        SetDirection();
        return NodeState.SUCCESS;
    }

    void SetDirection(){
        timesRun++;
        if(!navMovement.OnPath() || navMovement.GetNavMeshAgent().isStopped){
            Vector3 destination;
            if(directionToMove == Vector3.zero ){
                destination = FindPointOnArc(0f);
            }
            else{
                int chance = Random.Range(1,100);
                if(chance < 5 || navMovement.MoveToPreviousPoint()){
                    directionToMove *= -1f;
                }

                float angle = GetAngle(Vector3.forward, directionToMove);

                angle += Random.Range(-40, 40);
                destination = FindPointOnArc(angle);
            }

            
            directionToMove = destination - self.position;
            navMovement.SetDestination(destination);

            if (timesRun > 1){
                navMovement.SetMovetoPreviousPoint(false);
                timesRun = 0;
            }

            // navMovement.SetAutoBraking(true);
        }
    }

    Vector3 FindPointOnArc(float angle){
        Vector3 topPoint, pointOnArc;

        if(angle ==0f)
            angle = Random.Range(0, 360);
        angle *= Mathf.Deg2Rad;

        pointOnArc.x = self.position.x + (animalNeeds.viewRadius * Mathf.Sin(angle));
        pointOnArc.z = self.position.z + (animalNeeds.viewRadius * Mathf.Cos(angle));
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

        this.topPointG = topPoint;
        this.pointOnArcG = pointOnArc;

        return pointOnArc;

    }

    float GetAngle(Vector3 forward, Vector3 dir){
        Vector3 right;
        right = Vector3.Cross(dir, Vector3.up);
        dir = Vector3.Cross(Vector3.up, right);


        return Mathf.Atan2(Vector3.Dot(forward, right), Vector3.Dot(forward, dir)) * Mathf.Rad2Deg;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(topPointG, pointOnArcG);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(topPointG, 1f);
        Gizmos.DrawSphere(pointOnArcG, 1f);
        Gizmos.DrawSphere(self.position, 2f);
    }
}
