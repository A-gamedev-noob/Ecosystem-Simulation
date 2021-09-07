using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugs{
    public class DebugScript : MonoBehaviour{
        [SerializeField] Vector3 pointOnArc, topPoint, directionToMove;
        [SerializeField]AnimalNeedsScriptable animalNeeds;
        
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update(){
            if(Input.GetMouseButtonDown(0)){
                SetDirection();
                // FindPointOnArc(Random.Range(0,360));
                // PrintFor();
            }
        }

        void SetDirection()
        {
            
            Vector3 destination;
            if (directionToMove == Vector3.zero)
            {
                destination = FindPointOnArc(0f);
            }
            else
            {
                int chance = Random.Range(1, 100);
                if (chance < 25)
                {
                    directionToMove *= -1f;
                }
                float angle = Vector3.Angle(directionToMove, Vector3.forward);
                angle += Random.Range(-40, 40);
                destination = FindPointOnArc(angle);
            }
            directionToMove = destination - transform.position;
        }

        Vector3 FindPointOnArc(float angle)
        {
            if (angle == 0f)
                angle = Random.Range(0, 360);
            angle *= Mathf.Deg2Rad;

            pointOnArc.x = transform.position.x + (animalNeeds.viewRadius * Mathf.Sin(angle));
            pointOnArc.z = transform.position.z + (animalNeeds.viewRadius * Mathf.Cos(angle));
            pointOnArc.y = 100f;
            topPoint = pointOnArc;
            RaycastHit[] hits = Physics.RaycastAll(topPoint, Vector3.down, 200);

            if (hits != null)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        pointOnArc = hit.point;
                        print("p"); 
                    }
                }
            }

            return pointOnArc;

        }

        void PrintFor(){
            Debug.Log(transform.forward);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(topPoint, pointOnArc);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(topPoint,1f);
            Gizmos.DrawSphere(pointOnArc, 1f);
        }

    }
}
