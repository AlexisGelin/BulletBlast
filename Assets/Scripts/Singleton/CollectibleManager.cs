using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoSingleton<CollectibleManager>
{
    public List<Collectible> Collectibles;

    Collectible _colletcible;

    public Collectible GetRandomCollectible()
    {
        int chance = Random.Range(0, 100);

        if (chance <= 20) _colletcible = Collectibles[0];
        else if (chance <= 40) _colletcible = Collectibles[1];
        else _colletcible = Collectibles[2];


        return _colletcible;
    }
}
