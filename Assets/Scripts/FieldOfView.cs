using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public LayerMask targetMask;
    public LayerMask ObstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
    }
    public void FindClassicSelectableTile(int minRange, int maxRange, bool ldv)
    {
        visibleTargets.Clear();

        viewRadius = maxRange;

        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast (transform.position, dirToTarget, dstTarget, ObstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
        foreach (Transform visibleTarget in visibleTargets)
        {
            visibleTarget.GetComponent<Tile>().selectableAction = true; 
        }
    }
}
