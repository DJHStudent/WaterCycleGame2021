using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag
{

    //based on this explanation https://gamedevelopment.tutsplus.com/tutorials/shuffle-bags-making-random-feel-more-random--gamedev-1249
    List<int> shuffleList = new List<int>();
    int currPos;
    void createList()//the list of items created
    {
        ObjStats[] wall = GameManager.levelGen.wall;
        for (int i = 0; i < wall.Length; i++)
        {
            for (int j = 0; j < wall[i].spawnChance; j++)
                shuffleList.Add(i);
        }
        currPos = shuffleList.Count - 1;
        Debug.Log(shuffleList.Count);
    }
    public void initilize()
    {
        createList();
    }

    public int getNext()
    {
        if(currPos < 0)
        {
            currPos = shuffleList.Count - 1;
        }
        int randValue = Random.Range(0, currPos);
        //swapp the random item and the current item
        int temp = shuffleList[randValue];
        shuffleList[randValue] = shuffleList[currPos];
        shuffleList[currPos] = temp;

        currPos--;
        Debug.Log(temp);
        return temp;
    }
}
