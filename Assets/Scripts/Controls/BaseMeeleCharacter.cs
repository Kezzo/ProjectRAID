using Debug = UnityEngine.Debug;

public class BaseMeeleCharacter : BaseCharacter
{
    protected BaseCharacter m_lastAutoAttackTarget;

    /// <summary>
    /// Called when the automatic interaction happened.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        if (m_animator != null)
        {
            m_animator.SetTrigger("Attack");
        }

        m_lastAutoAttackTarget = targetToInteractWith;
    }

    /// <summary>
    /// Called when an automatic attack hit.
    /// </summary>
    protected virtual void OnAutoAttackHit()
    {
        if (m_lastAutoAttackTarget != null)
        {
            m_lastAutoAttackTarget.m_StatManagement.ChangeHealth(-m_AutoAttackDamage);
        }
        else
        {
            Debug.LogError("OnAutoAttackHit called without a valid target");
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();

        m_animator.SetBool("Attack", false);
    }
}
