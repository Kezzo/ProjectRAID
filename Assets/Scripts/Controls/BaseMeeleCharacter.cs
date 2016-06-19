using UnityEngine;

public class BaseMeeleCharacter : BaseCharacter
{
    [Header("BaseMeele")]

    [SerializeField]
    private Animator m_animator;

    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        m_animator.SetTrigger("Attack");
    }
}
