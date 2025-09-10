using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public float snapThreshold = 0.5f;          // Distance threshold for snapping
    public Transform correctSlot;               // Assign the correct slot in Inspector

    private bool isSnapped = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (!isSnapped)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset.z = 0; // Ensure we stay in the same Z plane
            Debug.Log("Mouse down - dragging started.");
        }
    }

    void OnMouseDrag()
    {
        if (!isSnapped)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Lock to 2D plane
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        if (!isSnapped)
        {
            float distance = Vector3.Distance(transform.position, correctSlot.position);
            Debug.Log($"OnMouseUp called. Distance to correct slot: {distance}");



            if (distance < snapThreshold)
            {
                transform.position = correctSlot.position;
                isSnapped = true;
                Debug.Log("✅ Piece snapped into place!");

                // 🔥 Tell PuzzleManager to check for completion
                if (PuzzleManager.Instance != null)
                    PuzzleManager.Instance.CheckPuzzleComplete();
            }

            else
            {
                Debug.Log("❌ Piece not close enough to snap.");
            }
        }
        else
        {
            Debug.Log("Piece already snapped. Ignoring further input.");
        }


    }

    public bool IsSnapped()
    {
        return isSnapped;
    }
}
