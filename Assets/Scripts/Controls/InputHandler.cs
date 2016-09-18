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

    private BaseCharacter m_currentlySelectedCharacter;

    public bool m_holdingLeftMouseButtonDown = false;

    // Update is called once per frame
    private void Update()
    {
        if (m_currentlySelectedCharacter != null && m_currentlySelectedCharacter.m_StatManagement.IsDead)
        {
            ResetSelection();
        }

        if (!Input.GetMouseButton(0))
        {
            if (m_currentlySelectedCharacter != null && m_holdingLeftMouseButtonDown)
            {
                InteractWithCharacterOrWorldAtMousePosition();
                ResetSelection();
            }
        }
        else if(m_holdingLeftMouseButtonDown)
        {
            RaycastHit selectionHit;
            if (TrySelection(m_camera, m_worldLayerMask, out selectionHit))
            {
                m_selectionPositionMarker.transform.position = new Vector3(selectionHit.point.x, m_selectionPositionMarker.transform.position.y, selectionHit.point.z);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            SelectCharacterAtMousePosition();
        }
    }

    /// <summary>
    /// Resets the current selection of a character.
    /// </summary>
    private void ResetSelection()
    {
        m_currentlySelectedCharacter = null;
        m_selectionMarker.SetActive(false);

        m_holdingLeftMouseButtonDown = false;
        m_selectionPositionMarker.SetActive(false);
    }

    /// <summary>
    /// Selects the character at mouse position.
    /// </summary>
    private void SelectCharacterAtMousePosition()
    {
        RaycastHit selectionTarget;

        if (TrySelection(m_camera, m_characterLayerMask, out selectionTarget))
        {
            GameObject selectionGameObject = selectionTarget.collider.gameObject;
            BaseCharacter baseCharacter = selectionGameObject.transform.parent.GetComponent<BaseCharacter>();

            if (baseCharacter != null && !baseCharacter.m_StatManagement.IsDead)
            {
                m_currentlySelectedCharacter = baseCharacter;
                baseCharacter.OnSelected();

                m_selectionMarker.SetActive(true);

                m_selectionMarker.transform.SetParent(selectionGameObject.transform);
                m_selectionMarker.transform.localPosition = Vector3.zero;
                m_selectionMarker.transform.localScale = Vector3.one;

                m_selectionPositionMarker.SetActive(true);

                m_holdingLeftMouseButtonDown = true;
            }
        }
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
