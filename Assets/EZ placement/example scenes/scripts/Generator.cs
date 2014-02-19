using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
	public GameObject pickup; //pickups which users can pick

    private Vector3 rndVector; //a variable to create random vector3 in it.
private int count;
    IEnumerator Start()
    {
        rndVector.y = 1;
        while (true)
        {
            rndVector.x = Random.Range(-3, 3);
            rndVector.z = Random.Range(-1, 6);
            rndVector.y = Random.Range(0.6f, 1.1f);
			count = Random.Range (15,20);
            Placement.CreateCircular(pickup, rndVector, 3, 2, (uint)count, false);
            yield return new WaitForSeconds(10);
        }
    }
}