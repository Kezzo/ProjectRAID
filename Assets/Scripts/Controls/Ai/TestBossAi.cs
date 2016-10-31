using System.Collections.Generic;
using UnityEngine;

public class TestBossAi : BaseAi
{
    private float m_lastSkill1CastTime;

    [SerializeField]
    private Transform m_skill1ProjectileStartPosition;

    [SerializeField]
    private GameObject m_skill1ProjectilePrefab;

    /// <summary>
    /// This gets called everytime the ai can decides to something.
    /// Normally scripts override this method.
    /// </summary>
    protected override void AiStep()
    {
        base.AiStep();

        if (m_CastingBlockedUntil > Time.time)
            return;

        if (CastBloodPoolProjectile())
            return;
        //if (CheckAndCastSkill1())
        //    return;
    }

    /// <summary>
    /// Casts the blood pool skill.
    /// </summary>
    /// <returns></returns>
    private bool CastBloodPoolProjectile()
    {
        if (!(m_lastSkill1CastTime + BaseBalancing.TestBossSkill1.m_Cooldown < Time.time))
        {
            return false;
        }

        m_lastSkill1CastTime = Time.time;

        List<BaseCharacter> possbileTargets = ControllerContainer.TargetingController.GetCharactersWithInteractionTarget(BaseBalancing.TestBossSkill1.m_PossibleTargets);

        if(possbileTargets == null || possbileTargets.Count == 0)
        {
            return false;
        }

        int randomElement = Random.Range(0, possbileTargets.Count - 1);
        BaseCharacter characterToTarget = possbileTargets[randomElement];

        GameObject projectileGameobject = Instantiate(m_skill1ProjectilePrefab, m_skill1ProjectileStartPosition.position, Quaternion.identity) as GameObject;

        if (projectileGameobject != null)
        {
            BaseProjectile projectileScript = projectileGameobject.GetComponent<BaseProjectile>();

            if (projectileScript != null)
            {
                projectileScript.InitializeBalancingParameter();
                projectileScript.FlyTowardsTarget(characterToTarget, m_characterToControl);
            }
        }

        return true;
    }

    /// <summary>
    /// Checks the and cast skill1.
    /// TODO: Unify and make this method generic and use in base class.
    /// </summary>
    /// <returns></returns>
    private bool CheckAndCastSkill1()
    {
        if (!(m_lastSkill1CastTime + BaseBalancing.TestBossSkill1.m_Cooldown < Time.time))
        {
            return false;
        }

        m_lastSkill1CastTime = Time.time;

        List<BaseCharacter> charactersToCastSkillOn = ControllerContainer.TargetingController.GetCharactersWithInteractionTarget(new HashSet<InteractionTarget>
        {
            InteractionTarget.Tank,
            InteractionTarget.Heal,
            InteractionTarget.Mage,
            InteractionTarget.Rogue
        });

        for (int characterIndex = 0; characterIndex < charactersToCastSkillOn.Count; characterIndex++)
        {
            GameObject projectileGameobject = Instantiate(m_skill1ProjectilePrefab, m_skill1ProjectileStartPosition.position, Quaternion.identity) as GameObject;

            if (projectileGameobject != null)
            {
                BaseProjectile projectileScript = projectileGameobject.GetComponent<BaseProjectile>();

                if (projectileScript != null)
                {
                    projectileScript.InitializeBalancingParameter();
                    projectileScript.FlyTowardsTarget(charactersToCastSkillOn[characterIndex], m_characterToControl);
                }
            }
        }

        return true;
    }
}
