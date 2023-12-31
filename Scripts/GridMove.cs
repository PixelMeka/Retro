using UnityEngine;

// animate a texture

public class GridMove : MonoBehaviour
{
    public float rateX;
    public float rateY;
    private Renderer r;
    private float offsetX;
    private float offsetY;
    private void Start()
    {
        r = GetComponent<Renderer>();
    }
    void Update()
    {
        offsetX += rateX * Time.deltaTime;
        offsetY += rateY * Time.deltaTime;
        r.materials[0].SetTextureOffset("_BaseMap", new Vector2(offsetX, offsetY));
    }
}
