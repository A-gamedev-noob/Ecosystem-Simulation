using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugs{
[ExecuteInEditMode]
    public class DebugScript : MonoBehaviour{
        [SerializeField] Vector3 pointOnArc, topPoint, directionToMove;
        [SerializeField]AnimalNeedsScriptable animalNeeds;
        [SerializeField] GameObject _object;
        [Range(0,360)]
        public float angle, _objectpos;
        
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update(){
            _object.transform.position = GetPointOnArc(_objectpos);
            if(Input.GetMouseButtonDown(0)){
                SetDirection();
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
                if (chance < 10)
                {
                    print("Bingo");
                    directionToMove *= -1f;
                }
                angle = Vector3.Angle(directionToMove, Vector3.forward);
                angle = GetAngle(Vector3.forward,directionToMove, Vector3.up);
                angle += Random.Range(-40, 40);
                destination = FindPointOnArc(angle);
            }
            directionToMove = destination - transform.position;
        }

        Vector3 FindPointOnArc(float _angle)
        {
            // if (angle == 0f)
            //     // angle = Random.Range(0, 360);
            _angle *= Mathf.Deg2Rad;

            pointOnArc.x = transform.position.x + (animalNeeds.viewRadius * Mathf.Sin(_angle));
            pointOnArc.z = transform.position.z + (animalNeeds.viewRadius * Mathf.Cos(_angle));
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
                        // print("p"); 
                    }
                }
            }

            return pointOnArc;

        }

        Vector3 GetPointOnArc(float _angle){

            _angle *= Mathf.Deg2Rad;

            Vector3 _pointOnArc, _topPoint;
            _pointOnArc.x = transform.position.x + (animalNeeds.viewRadius * Mathf.Sin(_angle));
            _pointOnArc.z = transform.position.z + (animalNeeds.viewRadius * Mathf.Cos(_angle));
            _pointOnArc.y = 100f;
            _topPoint = _pointOnArc;
            RaycastHit[] hits = Physics.RaycastAll(_topPoint, Vector3.down, 200);

            if (hits != null)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        _pointOnArc = hit.point;
                        // print("p"); 
                    }
                }
            }

            return _pointOnArc;
        }

        float GetAngle(Vector3 a, Vector3 forward, Vector3 axis){
            Vector3 right;
            right = Vector3.Cross(forward, axis);
            forward = Vector3.Cross(axis, right);


            return Mathf.Atan2(Vector3.Dot(a, right), Vector3.Dot(a, forward)) * Mathf.Rad2Deg;
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
