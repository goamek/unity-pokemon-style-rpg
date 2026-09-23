using UnityEngine;

// Something the player can interact with by facing it and pressing Z
public interface IInteractable
{
    void Interact(Transform initiator);
}
