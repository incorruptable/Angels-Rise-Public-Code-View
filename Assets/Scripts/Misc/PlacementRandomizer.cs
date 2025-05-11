using UnityEngine;
using System.Collections.Generic;

public class PlacementRandomizer : MonoBehaviour
{
    [SerializeField]
    float bufferDistance = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Vector2> positionsUsed = new List<Vector2>();
        bool nextPos = false;
        foreach (Transform childTransform in this.transform)
        {
            while (!nextPos)
            {
                float x = UnityEngine.Random.Range(-6.7f, 6.7f);
                float y = UnityEngine.Random.Range(0, 2) == 0 ? UnityEngine.Random.Range(-6.5f, -1*bufferDistance) : UnityEngine.Random.Range(bufferDistance, 6.5f);
                childTransform.localPosition = new Vector2(x, y);
                if (positionsUsed.Contains(childTransform.localPosition)) nextPos = false;
                else
                {
                    positionsUsed.Add(childTransform.localPosition);
                    nextPos = true;
                }
            }

            nextPos = false;
        }
    }

    //Adjust the Start to check if a position overlaps with another one in the layer. This component is to be attached to every layer (IE: Layer 1, Layer 2, Layer 3, Layer 1_0, Layer 2_1).
    //Items are 72 PPU, and 1 pixel = 1/72 WU which is about 0.01389 on the coordinate grid system. Half a sprite distance is 0.5 in coordinates, so minimum separation must be 1.0f.
}
