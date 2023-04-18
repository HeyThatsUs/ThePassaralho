using UnityEngine;

public class CenarioInfinitoControlador : MonoBehaviour 
{
 
    private Renderer _renderer;
    private Material _material;

    private float _offset;

    [SerializeField] 
    private float Velocidade = 1f;
    
    [SerializeField]
    private string sortingLayer = "Fundo";

    [SerializeField] 
    private int sortingOrder;
    

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.sortingOrder = sortingOrder;
        _renderer.sortingLayerName = sortingLayer;
        _material = _renderer.material;
        _material.renderQueue = 3000;
        

    }
    private void FixedUpdate()
    {
        _offset += 1;
        _material.SetTextureOffset("_MainTex", new Vector2((_offset * Velocidade) / 1000, 0));
        
    }


}
