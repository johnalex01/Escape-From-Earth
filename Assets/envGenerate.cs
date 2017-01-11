using UnityEngine;
using System.Collections;

public class envGenerate : MonoBehaviour {

    public Forest forest1;
    public Forest forest2;
    public GameObject[] forest;

    public int Forcount = 2;
    public void GenerateForest()
    {
        Forcount++;
        int type = Random.Range(0, 3);
        GameObject New_forest = GameObject.Instantiate(forest[type],new Vector3(0,0,Forcount*3000),Quaternion.identity) as GameObject;

        forest1 = forest2;
        forest2 = New_forest.GetComponent<Forest>();
    }

}
