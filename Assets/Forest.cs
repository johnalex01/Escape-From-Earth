using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    
    public float startLength = 50;
    private float minLen = 100;
    private float maxLen = 200;
    public GameObject[] obstacles;
    private Transform player;
    private waypoint wayPoints;
    private int TargetWayIndex;
    private envGenerate envGenerator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        wayPoints = transform.Find("waypoint").GetComponent<waypoint>();
        TargetWayIndex = wayPoints.Points.Length - 2;
        envGenerator = Camera.main.GetComponent<envGenerate>();
    }

	// Use this for initialization
	void Start () {
        GenerateObs();
	}
	
	// Update is called once per frame
	void Update () {
        //if (player.position.z > transform.position.z + 100)
        //{
        //    Camera.main.SendMessage("GenerateForest");
        //    GameObject.Destroy(this.gameObject);
        //}
	}

    void GenerateObs()
    {
        float startZ = transform.position.z - 3000;
        float endZ = transform.position.z;
        float z = startZ + startLength;
        while (true)
        {
            z += Random.Range(minLen, maxLen);
            if (z > endZ)
                break;
            else
            {
                //Vector3 offset=new Vector3(22,0,0);
                Vector3 pos = GetPosZ(z);

                int ObsIndex = Random.Range(0, obstacles.Length);

                GameObject go = GameObject.Instantiate(obstacles[ObsIndex], pos, Quaternion.identity) as GameObject;

                go.transform.parent = this.transform;
                
            }

        }
    }

    Vector3 GetPosZ(float z)
    {
        Transform[] Points=wayPoints.Points;
        int index=0;
        for (int i = Points.Length-1; i > 0; i--)
        {
            if (z > Points[i].position.z && z <= Points[i - 1].position.z)
            {
                index = i;
                break;
            }
        }

        return Vector3.Lerp(Points[index].position, Points[index-1].position, (z - Points[index].position.z) / (Points[index-1].position.z-Points[index].position.z));

        //return Vector3.zero;
    }

     public Vector3 getNextTarget()
    {
        while (true)
        {
            if (wayPoints.Points[TargetWayIndex].position.z - player.position.z < 10)
            {
                TargetWayIndex--;
                if (TargetWayIndex < 0)
                {
                    envGenerator.GenerateForest();
                    Destroy(this.gameObject, 1);
                    return envGenerator.forest1.getNextTarget();
                }
            }
            else
            {
                return wayPoints.Points[TargetWayIndex].position;
            }
        }
    }
}
