using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float duration = 4f;
    private Coroutine deactivateCoroutine;

    private void OnEnable()
    {
        // Stop any ongoing coroutine to avoid duplicates
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
        }

        // Start the deactivation coroutine
        deactivateCoroutine = StartCoroutine(DeactivateAfterDuration());
    }

    private void OnDisable()
    {
        // Ensure the coroutine is stopped when the object is deactivated manually
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            deactivateCoroutine = null;
        }
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
    }
}
