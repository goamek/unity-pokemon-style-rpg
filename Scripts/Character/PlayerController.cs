using UnityEngine;

// Reads player input for movement and interaction
public class PlayerController : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] Sprite sprite;

    Vector2 input;

    Character character;

    public string Name => playerName;
    public Sprite Sprite => sprite;
    public Character Character => character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    // Called by GameController while in the FreeRoam state
    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // No diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
                StartCoroutine(character.Move(input, OnMoveOver));
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    // Interacts with whatever is on the tile the player is facing
    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractableLayer);
        if (collider != null)
            collider.GetComponent<IInteractable>()?.Interact(transform);
    }

    // Checks for grass, trainer view, or portals under the player after each step
    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY), 0.2f, GameLayers.Instance.TriggerableLayers);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                character.Animator.IsMoving = false;
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }
}
