using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private LayerMask m_characterLayerMask;

    [SerializeField]
    private LayerMask m_interactionLayerMask;

    [SerializeField]
    private LayerMask m_worldLayerMask;

    [SerializeField]
    private GameObject m_selectionMarker;

    [SerializeField]
    private GameObject m_selectionPositionMarker;

    [Serializable]
    private class KeyBindAssignment
    {
        public KeyCode m_KeyCode;
        public BaseCharacter m_CharacterToSelect;
    }

    [SerializeField]
    private List<KeyBindAssignment> m_keyBindAssignments;

    private BaseCharacter m_currentlySelectedCharacter;

    // Update is called once per frame
    private void Update()
    {
        if (m_currentlySelectedCharacter != null && m_currentlySelectedCharacter.m_StatManagement.IsDead)
        {
            ResetSelection();
        }

        for (int keyBindAssignmentIndex = 0; keyBindAssignmentIndex < m_keyBindAssignments.Count; keyBindAssignmentIndex++)
        {
            if (Input.GetKeyDown(m_keyBindAssignments[keyBindAssignmentIndex].m_KeyCode))
            {
                BaseCharacter baseCharacter = m_keyBindAssignments[keyBindAssignmentIndex].m_CharacterToSelect;

                if (baseCharacter != null && !baseCharacter.m_StatManagement.IsDead)
                {
                    m_currentlySelectedCharacter = baseCharacter;
                    baseCharacter.OnSelected();

                    m_selectionMarker.SetActive(true);

                    m_selectionMarker.transform.SetParent(baseCharacter.transform);
                    m_selectionMarker.transform.localPosition = Vector3.zero;
                    m_selectionMarker.transform.localScale = Vector3.one;
                }
            }
        }

        if (m_currentlySelectedCharacter != null && Input.GetMouseButtonDown(1))
        {
            InteractWithCharacterOrWorldAtMousePosition();
        }
    }

    /// <summary>
    /// Resets the current selection of a character.
    /// </summary>
    private void ResetSelection()
    {
        m_currentlySelectedCharacter = null;
        m_selectionMarker.SetActive(false);
    }

    /// <summary>
    /// Interacts the with character or world at mouse position.
    /// </summary>
    private void InteractWithCharacterOrWorldAtMousePosition()
    {
        RaycastHit selectionHit;

        if (TrySelection(m_camera, m_interactionLayerMask, out selectionHit))
        {
            BaseCharacter baseCharacter = selectionHit.transform.parent.gameObject.GetComponent<BaseCharacter>();

            if (baseCharacter != null && !baseCharacter.m_StatManagement.IsDead)
            {
                m_currentlySelectedCharacter.OnInteraction(baseCharacter);
            }
        }
        else if (TrySelection(m_camera, m_worldLayerMask, out selectionHit))
        {
            m_currentlySelectedCharacter.MoveTo(selectionHit.point, 1f);
        }
    }

    /// <summary>
    /// Tries a selection.
    /// </summary>
    /// <param name="cameraToBaseSelectionOn">The camera to base selection on.</param>
    /// <param name="selectionMask">The selection mask.</param>
    /// <param name="selectionTarget">The selection target.</param>
    /// <returns></returns>
    private bool TrySelection(Camera cameraToBaseSelectionOn, LayerMask selectionMask, out RaycastHit selectionTarget)
    {
        Ray selectionRay = cameraToBaseSelectionOn.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        //To support orthographic cameras
        if (cameraToBaseSelectionOn.orthographic)
        {
            selectionRay.direction = cameraToBaseSelectionOn.transform.forward.normalized;
        }

        Debug.DrawRay(selectionRay.origin, selectionRay.direction * 100f, Color.yellow, 1f);

        return Physics.Raycast(selectionRay, out selectionTarget, 100f, selectionMask);
    }
}
