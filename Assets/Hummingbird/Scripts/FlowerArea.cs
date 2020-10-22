using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of flower plants and attached flowers
/// </summary>
public class FlowerArea : MonoBehaviour
{
    //The diameter of the area where the agent and flowers can be 
    //used for observing relative distance from agent to flower
    public const float AreaDiameter = 20f;
    
    //The list of all plants in this area (plants have multiple flowers)
    private List<GameObject> _flowerPlants;
    
    //Lookup dictionary for looking up a flower from a necatar collider
    private Dictionary<Collider, Flower> _nectarFlowerDictionary;

    /// <summary>
    /// List of all flowers in the flower area
    /// </summary>
    public List<Flower> Flowers { get; private set; }

    /// <summary>
    /// Reset the flowers and flower plants
    /// </summary>
    public void ResetFlowers()
    {
        //Rotate each flower plant around the y axis and subtly around x and z
        foreach (GameObject flowerPlant in _flowerPlants)
        {
            float xRot = UnityEngine.Random.Range(-5f, 5f);
            float yRot = UnityEngine.Random.Range(-180f, 180f);
            float zRot = UnityEngine.Random.Range(-5f, 5f);
            flowerPlant.transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
            
            //Reset each flower
            foreach (Flower flower in Flowers)
            {
                flower.ResetFLower();
            }
        }
    }

    /// <summary>
    /// Gets the <see cref="Flower"/> that a nectar collider belongs to 
    /// </summary>
    /// <param name="collider">The nectar collider</param>
    /// <returns>The matching flower</returns>
    public Flower GetFLowerFromNectar(Collider collider)
    {
        return _nectarFlowerDictionary[collider];
    }

    /// <summary>
    /// Called when the area wakes up
    /// </summary>
    private void Awake()
    {
        //Initialise variables
        _flowerPlants = new List<GameObject>();
        _nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>(); 
        
    }

    private void Start()
    {
        //Find all flowers that are children of this game object
        FindChildFlowers(transform);
    }

    /// <summary>
    /// Recursively all flowers and plants that are children of the parent transform 
    /// </summary>
    /// <param name="parent">PArent of the children to check</param>
    private void FindChildFlowers(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (CompareTag("flower_plant"))
            {
                //Found a flower plant add it to the list
                _flowerPlants.Add(child.gameObject);
                
                //Look for flowers within the flower plant
                FindChildFlowers(child);
            }
            else
            {
                //Not a flower plant, look for a flower plant
                Flower flower = child.GetComponent<Flower>();
                if (flower != null)
                {
                    //Found a flower, add to list
                    Flowers.Add(flower);
                    
                    //Add the nectar collider to the dictionary
                    _nectarFlowerDictionary.Add(flower.nectarCollider, flower);
                    
                    //Note: there are no flowers that are children of flowers
                }
                else
                {
                    //flower component not found
                    FindChildFlowers(child);
                }
            }
        }
        
    }
}
