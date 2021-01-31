using System;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace AI
{
    public class FieldOfView : MonoBehaviour
    {
        
        public float fieldOfViewAngle;
        public float lookRadius;
        [SerializeField] private GameObject _player;

        public bool PlayerSpotted { get; set; }

        public GameObject Player => _player;

        public float DistanceToPlayer => Vector3.Distance(_player.transform.position, transform.position);
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, lookRadius);

            Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfViewAngle, transform.up) * transform.forward * lookRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfViewAngle, transform.up) * transform.forward * lookRadius;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (_player.transform.position - transform.position).normalized * lookRadius);
            
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, transform.forward * lookRadius);
        }

        private void InView(Transform checkingObject, GameObject target, float maxAngle, float maxRadius)
        {
            Collider[] overlaps = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] == null) continue;
                if (!overlaps[i].CompareTag("Player")) continue;
                
                //Debug.Log("Target Found");
                Vector3 direction = (target.transform.position - checkingObject.position).normalized;
                direction.y *= 0;

                float angle = Vector3.Angle(checkingObject.forward, direction);

                if (!(angle <= maxAngle)) continue;
                
                Ray ray = new Ray(transform.position, 
                    _player.transform.position -transform.position);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, maxRadius)) continue;
                //Debug.Log("COntinued RC");
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("Player Spotted");
                }
            }
        }

        private void Detection()
        {
            Collider[] overlaps = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, overlaps);

            for (int i = 0; i < count + 1; i++)
            {
                if(overlaps[i] != null)
                {
                    if (overlaps[i].CompareTag("Player"))
                    {
                        //Debug.Log("Player In zone");
                        Vector3 direction = (_player.transform.position - transform.position).normalized;
                        direction.y *= 0;

                        float angle = Vector3.Angle(transform.forward, direction);

                        if (angle <= fieldOfViewAngle)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(transform.position, (_player.transform.position - transform.position).normalized, out hit))
                            {
                                //Debug.Log("Raycast Working");
                                //Debug.Log(hit.collider.tag);
                                if (hit.collider.CompareTag("Player"))
                                {
                                    //Debug.Log("Player Hit With Ray Cast");
                                    PlayerSpotted = true;
                                }
                            }
                        }
                    }
                };
            }
        }

        private void Update()
        {
            Detection();
        }
    }
}
