using UnityEngine;
using System.Collections;

public class waypoint : MonoBehaviour {

    public Transform[] Points;

    void OnDrawGizmos()
    {
        iTween.DrawPath(Points);
    }
}
