using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour
{
    protected float m_MinHitDistance = 0f;
    protected float m_ProjectileSpeed = 0f;

    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public virtual void InitializeBalancingParameter()
    {
        //Initialize character specific parameters in the child.
    }

    /// <summary>
    /// Called when a target has been hit.
    /// </summary>
    /// <param name="hitTargetCharacter">The hit target character.</param>
    protected virtual void OnTargetHit(BaseCharacter hitTargetCharacter)
    {
        
    }

    /// <summary>
    /// Flies the towards target.
    /// </summary>
    /// <param name="targetToFlyTowards">The target to fly towards.</param>
    public void FlyTowardsTarget(BaseCharacter targetToFlyTowards)
    {
        StartCoroutine(FlyTowardsTargetCoroutine(targetToFlyTowards, m_MinHitDistance, m_ProjectileSpeed));
    }

    /// <summary>
    /// Flies the towards target coroutine.
    /// </summary>
    /// <param name="targetToFlyTowards">The target to fly towards.</param>
    /// <param name="minHitDistance">The minimum hit distance.</param>
    /// <param name="projectileSpeed">The projectile speed.</param>
    /// <returns></returns>
    private IEnumerator FlyTowardsTargetCoroutine(BaseCharacter targetToFlyTowards, float minHitDistance, float projectileSpeed)
    {
        while (true)
        {
            //TODO: Implemented character size based distance activation.
            float distanceToTarget = Vector3.Distance(transform.position, targetToFlyTowards.transform.position);

            if (distanceToTarget <= minHitDistance)
            {
                //HealTarget(targetToFlyTowards);
                OnTargetHit(targetToFlyTowards);
            }
            else
            {
                float step = projectileSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetToFlyTowards.transform.position, step);
                transform.LookAt(targetToFlyTowards.transform.position);
            }

            yield return null;
        }
    }

}
