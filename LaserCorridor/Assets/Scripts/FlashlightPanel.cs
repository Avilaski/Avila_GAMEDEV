using UnityEngine;

public class FlashlightPanel : MonoBehaviour
{
    [Tooltip("Optional target on the panel surface. Otherwise uses renderer bounds center.")]
    [SerializeField] Transform revealPoint;
    [SerializeField] float revealDelay = 2f;
    float revealedUntil = float.NegativeInfinity;
    Renderer[] visuals;
    bool[] originallyEnabled;

    void Awake()
    {
        visuals = GetComponentsInChildren<Renderer>(true);
        originallyEnabled = new bool[visuals.Length];

        for (int i = 0; i < visuals.Length; i++)
        {
            originallyEnabled[i] = visuals[i].enabled;
            visuals[i].enabled = false;
        }
    }

    public bool IsIlluminated()
    {
        if (!isActiveAndEnabled)
        {
            return false;
        }

        Vector3 target = transform.position;

        if (revealPoint != null)
        {
            target = revealPoint.position;
        }
        else if (visuals.Length > 0)
        {
            target = visuals[0].bounds.center;
        }

        if (PlayerFlashlight.Active != null && PlayerFlashlight.Active.Illuminates(target, transform))
        {
            revealedUntil = Time.time + revealDelay;
            return true;
        }

        return Time.time < revealedUntil;
    }

    void LateUpdate()
    {
        bool visible = IsIlluminated();

        for (int i = 0; i < visuals.Length; i++)
        {
            if (visuals[i] != null)
            {
                visuals[i].enabled = visible && originallyEnabled[i];
            }
        }
    }
}
