using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    // Assign the Cloth component in the inspector
    public Cloth flagCloth;

    // This method should be called when the wind direction or speed changes
    public void UpdateWind(Vector3 wind)
    {
        if (flagCloth != null)
        {
            // Update the externalAcceleration of the Cloth component
            flagCloth.externalAcceleration = wind;
        }
    }
}