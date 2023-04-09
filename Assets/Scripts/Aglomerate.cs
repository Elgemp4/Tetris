using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aglomerate
{
    private Playfield playfield;

    public List<GameObject> blockList = new List<GameObject>();

    public Aglomerate(List<GameObject> blockList) 
    {
        playfield = Playfield.Instance;

        this.blockList = blockList;
    }

    public void Drop() 
    {
        while (!HasLanded())
        { 
            MoveDown();
        }
    }

    private void MoveDown()
    { 
        foreach (GameObject block in blockList) 
        {
            block.transform.position += Vector3.down;
        }
    }

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
