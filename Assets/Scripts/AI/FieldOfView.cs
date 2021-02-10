using System;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace AI
{
    public class FieldOfView : MonoBehaviour
    {

        public Transform fovOrigin;
        public float fieldOfViewAngle;
        public float lookRadius;
        private GameObject _player;
        [SerializeField] private Transform _rayCastOrigin;
        
        public bool PlayerSpotted { get; set; }

        public GameObject Player => _player;

        public float DistanceToPlayer => Vector3.Distance(_player.transform.position, transform.position);
        
        //To be deleted once debugging is no longer needed, that is the only purpose of this function
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, lookRadius);

            Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfViewAngle, fovOrigin.up) * fovOrigin.forward * lookRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfViewAngle, fovOrigin.up) * fovOrigin.forward * lookRadius;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(fovOrigin.position, fovLine1);
            Gizmos.DrawRay(fovOrigin.position, fovLine2);
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_rayCastOrigin.position, (_player.transform.position - fovOrigin.position).normalized * lookRadius);
            
            Gizmos.color = Color.black;
            Gizmos.DrawRay(_rayCastOrigin.position, fovOrigin.forward * lookRadius);
        }

       private void Detection()
        {
            Collider[] overlaps = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(fovOrigin.position, lookRadius, overlaps);

            for (int i = 0; i < count; i++)
            {
                if(overlaps[i] != null)
                {
                    if (overlaps[i].CompareTag("Player"))
                    {
                        Vector3 direction = (_player.transform.position - fovOrigin.position).normalized;
                        direction.y *= 0;

                        float angle = Vector3.Angle(fovOrigin.forward, direction);

                        if (angle <= fieldOfViewAngle)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(_rayCastOrigin.position, (_player.transform.position - fovOrigin.position).normalized, out hit))
                            {
                                if (hit.collider.CompareTag("Player"))
                                {
                                    PlayerSpotted = true;
                                }
                            }
                        }
                    }
                };
            }
        }
       
       private void Awake()
       {
           _player = GameObject.FindWithTag("Player");
       }
       
       private void Update()
        {
            Detection();
        }
    }
}
