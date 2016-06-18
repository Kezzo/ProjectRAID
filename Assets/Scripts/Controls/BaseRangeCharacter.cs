using UnityEngine;
using System.Collections;

public class BaseRangeCharacter : BaseCharacter
{
    [SerializeField]
    private GameObject m_autoInteractionProjectilePrefab;

    /// <summary>
    /// Called when an interaction happened.
    /// </summary>
    /// <param name="characterInteractingWith">The character interacting with.</param>
    public override void OnInteraction(BaseCharacter characterInteractingWith)
    {
        base.OnInteraction(characterInteractingWith);

        StartAutoInteraction(characterInteractingWith);
    }

    /// <summary>
    /// Starts the automatic heal.
    /// </summary>
    /// <param name="characterToInteractWith">The character to heal.</param>
    private void StartAutoInteraction(BaseCharacter characterToInteractWith)
    {
        if (m_CurrentInteractionCoroutine != null)
        {
            StopCoroutine(m_CurrentInteractionCoroutine);
        }

        m_CurrentInteractionCoroutine = StartCoroutine(RangeAutoInteractionCoroutine(characterToInteractWith, m_AutoInteractionCD, m_autoInteractionProjectilePrefab));
    }

    /// <summary>
    /// The continuous heal coroutine.
    /// </summary>
    /// <param name="targetToInteractWith">The target to heal.</param>
    /// <param name="autoInteractionCd">The automatic interaction cd.</param>
    /// <param name="projectile">The projectile.</param>
    /// <returns></returns>
    protected IEnumerator RangeAutoInteractionCoroutine(BaseCharacter targetToInteractWith, float autoInteractionCd, GameObject projectile)
    {
        while (true)
        {
            if (m_TimeSinceLastAutoInteraction >= autoInteractionCd)
            {
                m_TimeSinceLastAutoInteraction = 0f;

                GameObject projectileGameobject = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;

                if (projectileGameobject != null)
                {
                    BaseProjectile projectileScript = projectileGameobject.GetComponent<BaseProjectile>();

                    if (projectileScript != null)
                    {
                        projectileScript.InitializeBalancingParameter();
                        projectileScript.FlyTowardsTarget(targetToInteractWith);
                    }
                }

                //get Heal projectile component
            }

            yield return null;
        }
    }
}
