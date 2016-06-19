using UnityEngine;
using System.Collections;

public class BaseMeeleCharacter : BaseCharacter
{
    [SerializeField]
    private Animator m_animator;

    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        m_animator.SetTrigger("Attack");
    }
}
