using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoSingleton<CollectibleManager>
{
    public List<Collectible> Collectibles;

    public Collectible GetRandomCollectible()
    {
        return Collectibles[Random.Range(0, Collectibles.Count)];
    }
}
