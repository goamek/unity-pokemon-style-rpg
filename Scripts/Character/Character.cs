using System;
using System.Collections;
using UnityEngine;

// Shared tile-based movement for the player, NPCs, and trainers
public class Character : MonoBehaviour
{
    public float moveSpeed;

    CharacterAnimator animator;

    public bool IsMoving { get; private set; }

    // Raises the sprite so its feet sit in the middle of the tile
    public float OffsetY { get; private set; } = 0.3f;

    public CharacterAnimator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        SetPositionAndSnapToTile(transform.position);
    }

    public void SetPositionAndSnapToTile(Vector2 position)
    {
        position.x = Mathf.Floor(position.x) + 0.5f;
        position.y = Mathf.Floor(position.y) + 0.5f + OffsetY;

        transform.position = position;
    }

    // Walks by moveVec tiles if the path is clear, then invokes OnMoveOver
    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver = null)
    {
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!IsPathClear(targetPos))
            yield break;

        IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        IsMoving = false;

        OnMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    // Box-casts from the next tile to the target so the character's own collider is skipped
    private bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;
        var blockingLayers = GameLayers.Instance.SolidLayer | GameLayers.Instance.InteractableLayer | GameLayers.Instance.PlayerLayer;

        return !Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, blockingLayers);
    }

    // Faces the target; only works for targets in the same row or column
    public void LookTowards(Vector3 targetPos)
    {
        var xdiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if (xdiff == 0 || ydiff == 0)
        {
            animator.MoveX = Mathf.Clamp(xdiff, -1f, 1f);
            animator.MoveY = Mathf.Clamp(ydiff, -1f, 1f);
        }
        else
        {
            Debug.LogError("LookTowards: target must share a row or column");
        }
    }
}
