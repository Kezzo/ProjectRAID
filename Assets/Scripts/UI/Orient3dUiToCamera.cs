using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Orient3dUiToCamera : MonoBehaviour
{
    [SerializeField]
    private Camera m_cameraToOrientTo;

	// Update is called once per frame
	void Update ()
    {
        Vector3 v = m_cameraToOrientTo.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(m_cameraToOrientTo.transform.position - v);
	    transform.rotation = m_cameraToOrientTo.transform.rotation;
    }
}
