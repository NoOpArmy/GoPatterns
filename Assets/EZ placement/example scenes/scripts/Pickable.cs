using UnityEngine;

public class Pickable : MonoBehaviour
{
	public Color[] colors;
	
	void Awake()
	{
		renderer.material.color = colors[Random.Range(0,colors.Length)];
	}
	

    void OnMouseEnter()
    {
        Destroy(this.gameObject);
        if (Random.value < 0.03)
        {
            Placement.CreateCubical(this.gameObject, null, transform.position, 3, 1, 3, 1.5f, Placement.FillMode.empty);
        }
    }
}