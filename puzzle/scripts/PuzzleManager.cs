using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; // Singleton reference

    [System.Serializable]
    public class PieceSlotPair
    {
        public PuzzlePiece piece;   // Puzzle piece
        public Transform slot;      // Matching slot
    }

    public PieceSlotPair[] pairs; // Assign in Inspector
    public GameObject winPanel;   // UI panel to show when puzzle completes

    void Awake()
    {
        // Make this object globally accessible
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Hide win panel at start
        if (winPanel != null)
            winPanel.SetActive(false);

        // Link pieces to their slots
        foreach (var pair in pairs)
        {
            if (pair.piece != null && pair.slot != null)
            {
                pair.piece.correctSlot = pair.slot;
            }
            else
            {
                Debug.LogWarning("PuzzleManager: A pair is missing either the piece or the slot.");
            }
        }
    }

    public void CheckPuzzleComplete()
    {
        for (int i = 0; i < pairs.Length; i++)
        {
            var pair = pairs[i];

            if (pair.piece == null || ReferenceEquals(pair.piece, null))
            {
                Debug.LogWarning($"❌ PuzzleManager: Null or missing piece at index [{i}] — Slot: {(pair.slot != null ? pair.slot.name : "null")}");
                return;
            }

            if (!pair.piece.IsSnapped())
            {
                Debug.Log($"⏳ Pair [{i}] piece is not snapped yet.");
                return;
            }
        }

        // Puzzle is complete
        Debug.Log("🎉 Puzzle Completed!");
        if (winPanel != null)
            winPanel.SetActive(true);
    }


}
