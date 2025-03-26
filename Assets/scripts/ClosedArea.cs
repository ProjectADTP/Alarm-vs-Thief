using System;
using UnityEngine;

public class ClosedArea : MonoBehaviour
{
    public event Action<ClosedArea> Entered;
    public event Action<ClosedArea> Exited;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out Mover component) == false)
            return;

        Entered?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Mover>(out Mover component) == false)
            return;

        Exited?.Invoke(this);
    }
}
