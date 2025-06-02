using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AddBackgroundCollider : MonoBehaviour
{
    [Tooltip("Thickness of the boundary colliders")]
    public float colliderThickness = 0.1f;

    [Tooltip("Optional parent transform for the generated boundary colliders")]
    public Transform boundaryParent;

    [ContextMenu("Add Boundary Colliders")]
    private void AddBoundaryColliders()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
            return;
        }

        // Get the size of the sprite
        Vector2 spriteSize = spriteRenderer.size;

        // Create colliders for each side of the sprite
        CreateCollider(new Vector2(-spriteSize.x / 2, 0), new Vector2(colliderThickness, spriteSize.y)); // Left
        CreateCollider(new Vector2(spriteSize.x / 2, 0), new Vector2(colliderThickness, spriteSize.y)); // Right
        CreateCollider(new Vector2(0, -spriteSize.y / 2), new Vector2(spriteSize.x, colliderThickness)); // Bottom
        CreateCollider(new Vector2(0, spriteSize.y / 2), new Vector2(spriteSize.x, colliderThickness)); // Top
    }
    private void CreateCollider(Vector2 offset, Vector2 size)
    {
        GameObject colliderObject = new GameObject("BoundaryCollider");
        BoxCollider2D boxCollider = colliderObject.AddComponent<BoxCollider2D>();
        boxCollider.size = size;
        boxCollider.isTrigger = false;

        // Set the position of the collider
        colliderObject.transform.position = transform.position + (Vector3)offset;

        // Set the parent if specified
        if (boundaryParent != null)
        {
            colliderObject.transform.SetParent(boundaryParent);
        }
    }
}
