using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 StartPosition; 
    [SerializeField] public GameObject obj;
    public float speed;
    void Start()
    {
        StartPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Trail.trailNum == 0)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/45f), StartPosition.y, StartPosition.z);
        }
        else if(Trail.trailNum == 1)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/60f), StartPosition.y, StartPosition.z);
        }
        else if(Trail.trailNum == 2)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/90f), StartPosition.y, StartPosition.z);
        }
        else if(Trail.trailNum == 3)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/120f), StartPosition.y, StartPosition.z);
        }
        else if(Trail.trailNum == 4)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/120f), StartPosition.y, StartPosition.z);
        }
        else if(Trail.trailNum == 5)
        {
            transform.position = new Vector3(Mathf.Lerp(StartPosition.x, obj.transform.position.x, Trail.progress/15f), StartPosition.y, StartPosition.z);
        }
                
    }
}
