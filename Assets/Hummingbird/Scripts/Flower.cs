using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a flower with necter
/// </summary>
public class Flower : MonoBehaviour
{
    [Tooltip("Colour when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, 0.3f);

    [Tooltip("Colour when the flower is empty")]
    public Color emptyFlowerColor = new Color(0.5f, 0f, 1f);

    /// <summary>
    /// Trigger collider representing the nectar
    /// </summary>
    [HideInInspector] public Collider nectarCollider;

    //Solid collider representing the flower petals 
    private Collider _flowerCollider;

    //Flowers material
    private Material _flowerMaterial;

    /// <summary>
    /// A Vector pointing straight out of the flower
    /// </summary>
    public Vector3 FlowerUpVector => nectarCollider.transform.up;

    /// <summary>
    /// The centre position of the nectar collider
    /// </summary>
    public Vector3 FlowerCentrePosition => nectarCollider.transform.position;
    
    /// <summary>
    /// Amount of nectar remaining in the flower
    /// </summary>
    public float NectarAmount { get; private set;}

    /// <summary>
    /// Does the flower still have nectar
    /// </summary>
    public bool HasNectar => NectarAmount > 0f;

    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount">Amount of nectar to remove</param>
    /// <returns>The actual amount successfully removed</returns>
    public float Feed(float amount)
    {
        //Track how much nectar was successfully taken (cannot take more than is available)
        float taken = Mathf.Clamp(amount, 0f, NectarAmount);
        
        //Subtract the nectar
        NectarAmount -= amount;

        if (NectarAmount <= 0)
        {
            //No nectar remaining
            NectarAmount = 0;
            
            //Disable the flower and nectar colliders
            _flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);
            
            //Change the flower colour
            _flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);
        }
        //Return the amount of nectar taken
        return taken;
    }

    /// <summary>
    /// Resets the flower colour, nectar amount and colliders
    /// </summary>
    public void ResetFlower()
    {
        //Refill Nectar
        NectarAmount = 1f;
        
        //Enable colliders
        _flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);
        
        //Change flower colour
        _flowerMaterial.SetColor("_BaseColor", fullFlowerColor);
    }

    private void Awake()
    {
        //Find the flowers mesh renderer and get the main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        _flowerMaterial = meshRenderer.material;
        
        //Find flower and nectar colliders
        _flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();
    }
}