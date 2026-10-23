using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Light))]
public class PlayerFlashlight : MonoBehaviour
{
    public static PlayerFlashlight Active { get; private set; }
    [SerializeField] bool startsOn = false;
    [Tooltip("Walls and cover only. Exclude Player and Enemy layers.")]
    [SerializeField] LayerMask obstacleLayers = 1;
    Light beam;

    void Awake()
    {
        beam = GetComponent<Light>();
        beam.type = LightType.Spot;
        beam.enabled = startsOn;
    }

    void OnEnable() 
    { 
        Active = this; 
    }
    void OnDisable() 
    {
        if (Active == this)
        {
            Active = null;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            beam.enabled = !beam.enabled;
        }

    }

    public bool Illuminates(Vector3 point, Transform panel)
    {
        if (!isActiveAndEnabled || !beam.isActiveAndEnabled || beam.intensity <= 0f)
        {
            return false;
        }

        Vector3 offset = point - beam.transform.position;
        float distance = offset.magnitude;

        if (distance > beam.range)
        {
            return false;
        }

        if (distance < 0.001f)
        {
            return true;
        }

        if (Vector3.Angle(beam.transform.forward, offset) > beam.spotAngle * 0.5f)
        {
            return false;
        }

        // Ignore this panel's own colliders, but let other solid covers block sight
        foreach (RaycastHit hit in Physics.RaycastAll(beam.transform.position, offset / distance, distance, obstacleLayers, QueryTriggerInteraction.Ignore))
        {
            if (!hit.transform.IsChildOf(panel)) return false;
        }

        return true;
    }
}
