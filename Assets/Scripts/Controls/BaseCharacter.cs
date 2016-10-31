using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionTarget
{
    None,
    Tank,
    Rogue,
    Heal,
    Mage,
    Boss,
    Add
}

public class BaseCharacter : MonoBehaviour
{
    [Header("Base")]

    [SerializeField]
    public BaseStatManagement m_StatManagement;

    [HideInInspector]
    public string m_CharacterId;

    [SerializeField]
    protected Animator m_animator;

    private Coroutine m_currentMovementCoroutine;
    protected Coroutine m_CurrentInteractionCoroutine;

    protected InteractionTarget m_InteractionTarget;
    public InteractionTarget InteractionTarget
    {
        get { return m_InteractionTarget; }
    }

    protected HashSet<InteractionTarget> m_PossibleInteractionTargets;
    public HashSet<InteractionTarget> PossibleInteractionTargets
    {
        get { return m_PossibleInteractionTargets; }
    }

    protected float m_TimeSinceLastAutoInteraction;

    protected float m_MovementSpeed;
    protected float m_AutoInteractionMaxRange;
    protected float m_AutoInteractionCd;

    protected int m_AutoAttackDamage;

    void Awake()
    {
        InitializeBalancingParameter();

        ControllerContainer.TargetingController.RegisterInTargetCache(this);

        if (m_animator != null)
        {
            m_animator.logWarnings = false;
        }
    }

    void Start()
    {
        m_StatManagement.m_OnDead += HandleDeath;
    }

    void Update()
    {
        m_TimeSinceLastAutoInteraction += Time.deltaTime;
    }

    /// <summary>
    /// Sets the layer to dead on death.
    /// </summary>
    private void HandleDeath()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Dead");

        if (m_animator != null)
        {
            //TODO: Play death animation here.
            //m_animator.enabled = false;
            //m_animator.enabled = true;
            m_animator.SetTrigger("Death");
        } 
    }

    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public virtual void InitializeBalancingParameter()
    {
        //Initialize character specific balancing parameters in the child.
    }

    /// <summary>
    /// Called when the character was selected.
    /// </summary>
    public virtual void OnSelected()
    {
        //Debug.Log("Selected Character: "+this.name);
    }

    #region interaction

    /// <summary>
    /// Called when an interaction with a target happened.
    /// </summary>
    /// <param name="interactionTarget">The interaction target.</param>
    public virtual void OnInteraction(BaseCharacter interactionTarget)
    {
        //Debug.Log(string.Format("'{0}' is interacting with '{1}'", name, interactionTarget.name));

        if (Vector3.Distance(transform.position, interactionTarget.transform.position) > m_AutoInteractionMaxRange)
        {
            MoveTo(interactionTarget, m_AutoInteractionMaxRange, () => StartAutoInteraction(interactionTarget));
        }
        else
        {
            StartAutoInteraction(interactionTarget);
        }
    }

    /// <summary>
    /// Starts the automatic heal.
    /// </summary>
    /// <param name="characterToInteractWith">The character to heal.</param>
    private void StartAutoInteraction(BaseCharacter characterToInteractWith)
    {
        //Debug.Log(string.Format("StartAutoInteraction: '{0}' with '{1}'", this.name, characterToInteractWith.name));

        StopMovement();
        LookAt(characterToInteractWith.transform.position);

        if (m_CurrentInteractionCoroutine != null)
        {
            StopCoroutine(m_CurrentInteractionCoroutine);
        }

        m_CurrentInteractionCoroutine = StartCoroutine(AutoInteractionCoroutine(characterToInteractWith, m_AutoInteractionMaxRange, m_AutoInteractionCd));
    }


    /// <summary>
    /// Stops the interaction.
    /// </summary>
    public void StopInteraction()
    {
        if (m_CurrentInteractionCoroutine != null)
        {
            StopCoroutine(m_CurrentInteractionCoroutine);
        }
    }

    /// <summary>
    /// Called when the automatic interaction happened.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected virtual void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        //Debug.Log(string.Format("OnAutoInteractionTriggered from '{0}' on '{1}'", this.name, targetToInteractWith.name));
    }

    /// <summary>
    /// The continuous heal coroutine.
    /// </summary>
    /// <param name="targetToInteractWith">The target to heal.</param>
    /// <param name="autoInteractionMaxRange">The automatic interaction maximum range.</param>
    /// <param name="autoInteractionCd">The automatic interaction cd.</param>
    /// <returns></returns>
    private IEnumerator AutoInteractionCoroutine(BaseCharacter targetToInteractWith, float autoInteractionMaxRange, float autoInteractionCd)
    {
        while (true)
        {
            if (m_StatManagement.IsDead || targetToInteractWith.m_StatManagement.IsDead)
            {
                if (m_animator != null)
                {
                    m_animator.SetBool("Attack", false);
                }

                yield break;
            }

            LookAt(targetToInteractWith.transform.position);

            if (Vector3.Distance(transform.position, targetToInteractWith.transform.position) <= autoInteractionMaxRange)
            {
                if (m_TimeSinceLastAutoInteraction >= autoInteractionCd &&
                m_PossibleInteractionTargets.Contains(targetToInteractWith.m_InteractionTarget))
                {
                    m_TimeSinceLastAutoInteraction = 0f;

                    OnAutoInteractionTriggered(targetToInteractWith);
                }
            }
            else
            {
                //Debug.Log(string.Format("Target '{0}' from '{1}' ran out of Range", targetToInteractWith.name, this.name));

                if (m_animator != null)
                {
                    m_animator.SetBool("Attack", false);
                }

                MoveTo(targetToInteractWith, autoInteractionMaxRange,
                    () => StartAutoInteraction(targetToInteractWith));
            }

            yield return null;
        }
    }

    #endregion

    #region movement

    /// <summary>
    /// Moves to.
    /// </summary>
    /// <param name="worldPositionToMoveTo">The world position to move to.</param>
    /// <param name="targetReachedMinDistance">The target reached minimum distance.</param>
    /// <param name="onTargetReached">The on target reached.</param>
    public void MoveTo(Vector3 worldPositionToMoveTo, float targetReachedMinDistance, Action onTargetReached = null)
    {
        //Debug.Log(string.Format("MoveTo '{0}' moves to '{1}'", this.name, worldPositionToMoveTo));

        //Debug.Log("Character: "+this.name+" moves to "+ worldPositionToMoveTo);

        //Stop previous movement.
        StopMovement();

        //Stop current interaction.
        StopInteraction();

        m_currentMovementCoroutine = StartCoroutine(MovementCoroutine(worldPositionToMoveTo, targetReachedMinDistance, onTargetReached));
    }

    /// <summary>
    /// Moves to a specific character.
    /// </summary>
    /// <param name="characterToMoveTo">The character to move to.</param>
    /// <param name="targetReachedMinDistance">The target reached minimum distance. If -1, will follow target forever.</param>
    /// <param name="onTargetReached">The on target reached.</param>
    public void MoveTo(BaseCharacter characterToMoveTo, float targetReachedMinDistance, Action onTargetReached = null)
    {
        //Debug.Log(string.Format("MoveTo '{0}' moves to '{1}'", this.name, characterToMoveTo.transform.position));

        //Debug.Log("Character: "+this.name+" moves to "+ worldPositionToMoveTo);

        //Stop previous movement.
        StopMovement();

        //Stop current interaction.
        StopInteraction();

        m_currentMovementCoroutine = StartCoroutine(MovementCoroutine(characterToMoveTo, targetReachedMinDistance, onTargetReached));
    }

    /// <summary>
    /// Stops the movement.
    /// </summary>
    private void StopMovement()
    {
        if (m_animator != null)
        {
            m_animator.SetBool("Walk", false);
        }

        if (m_currentMovementCoroutine != null)
        {
            StopCoroutine(m_currentMovementCoroutine);
        }
    }

    /// <summary>
    /// Movements the numerator.
    /// </summary>
    /// <param name="movementTarget">The movement target.</param>
    /// <param name="targetReachedMinDistance">The target reached minimum distance.</param>
    /// <param name="onTargetReached">The on target reached.</param>
    /// <returns></returns>
    private IEnumerator MovementCoroutine(Vector3 movementTarget, float targetReachedMinDistance, Action onTargetReached = null)
    {
        if (m_animator != null)
        {
            m_animator.SetBool("Walk", true);
        }

        while (true)
        {
            bool reachedTarget = DoMovementStep(movementTarget, targetReachedMinDistance, onTargetReached);

            if (reachedTarget || m_StatManagement.IsDead)
            {
                StopMovement();

                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Movements the numerator.
    /// </summary>
    /// <param name="characterToFollow">The character to follow.</param>
    /// <param name="targetReachedMinDistance">The target reached minimum distance.</param>
    /// <param name="onTargetReached">The on target reached.</param>
    /// <returns></returns>
    private IEnumerator MovementCoroutine(BaseCharacter characterToFollow, float targetReachedMinDistance, Action onTargetReached = null)
    {
        if (m_animator != null)
        {
            m_animator.SetBool("Walk", true);
        }

        while (true)
        {
            bool reachedTarget = DoMovementStep(characterToFollow.transform.position, targetReachedMinDistance, onTargetReached);

            if (reachedTarget || m_StatManagement.IsDead || characterToFollow.m_StatManagement.IsDead)
            {
                StopMovement();

                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Does the movement step.
    /// </summary>
    /// <param name="movementTarget">The movement target.</param>
    /// <param name="targetReachedMinDistance">The target reached minimum distance.</param>
    /// <param name="onTargetReached">The on target reached.</param>
    /// <returns></returns>
    private bool DoMovementStep(Vector3 movementTarget, float targetReachedMinDistance, Action onTargetReached = null)
    {
        LookAt(movementTarget);

        float currentDistance = Vector3.Distance(transform.position, movementTarget);
        float step = m_MovementSpeed * Time.deltaTime;

        if (currentDistance < targetReachedMinDistance)
        {
            if (onTargetReached != null)
            {
                onTargetReached();
            }

            return true;
        }

        transform.position = Vector3.MoveTowards((new Vector3(transform.position.x, 0f, transform.position.z)),
            (new Vector3(movementTarget.x, 0f, movementTarget.z)), step);

        return false;
    }

    #endregion

    /// <summary>
    /// Looks at.
    /// </summary>
    /// <param name="postitionToLookAt">The postition to look at.</param>
    private void LookAt(Vector3 postitionToLookAt)
    {
        //To not turn to yourself, that's pretty hard
        if (this.transform.position == postitionToLookAt)
            return;

        transform.rotation = Quaternion.LookRotation((new Vector3(postitionToLookAt.x, 0f, postitionToLookAt.z) - 
            (new Vector3(transform.position.x, 0f, transform.position.z))).normalized);
    }
}
