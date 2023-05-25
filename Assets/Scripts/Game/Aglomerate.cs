using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La classe Aglomerate représente un ensemble de blocs qui sont lié l'un à l'autre. 
/// </summary>
public class Aglomerate
{
    private Playfield playfield;

    public List<GameObject> blockList = new List<GameObject>();

    /// <summary>
    /// Constructeur de la class <c>Aglomerate</c>.
    /// </summary>
    /// <param name="blockList">Un groupe de bloc liés entre eux</param>
    public Aglomerate(List<GameObject> blockList) 
    {
        playfield = Playfield.Instance;

        this.blockList = blockList;
    }

    /// <summary>
    /// Fait descendre tous les blocs jusqu'à ce qu'ils atterissent.
    /// </summary>
    public void Drop() 
    {
        while (!HasLanded())
        { 
            MoveDown();
        }
    }

    /// <summary>
    /// Fait descendre tous les blocs d'une case.
    /// </summary>
    private void MoveDown()
    { 
        foreach (GameObject block in blockList) 
        {
            block.transform.position += Vector3.down;
        }
    }

    /// <summary>
    /// Calcule si le groupe de blocs est arrivé en bas du plateau de jeu ou s'il est posé sur un autre groupe de blocs.
    /// </summary>
    /// <returns>Vrai si il atteri, faux si il est encore dans les airs</returns>
    public bool HasLanded()
    { 
        foreach(GameObject block in blockList) 
        { 
            Vector3 blockPosition = block.transform.position;

            if(blockPosition.y == 0 || playfield.IsABlockPresent((int)blockPosition.x, (int)blockPosition.y-1))
            {
                return true;
            }
        }

        return false;
    }
}
