using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag
{

    //based on this explanation https://gamedevelopment.tutsplus.com/tutorials/shuffle-bags-making-random-feel-more-random--gamedev-1249
    List<int> shuffleList = new List<int>(); //list of all elements wanting to pick from
    int currPos;
    public void createList()//the list of items created
    { //
        ObjStats[] plats = GameManager.levelGen.wall;
        int[,] wall = GameManager.levelGen.levelPacing_ElementSpawnChance;

        for (int i = 0; i < wall.Length; i++)
        {
            for (int j = 1; j < wall[i].Length; j++)
                shuffleList.Add(i);
        }
        currPos = shuffleList.Count - 1;
    }

    public void createTypes()//the list of items created
    {
        int[,] wall = GameManager.levelGen.levelPacing_ElementSpawnChance;
        for (int i = 0; i < wall.Length; i++)
        {
            for (int j = 0; j < wall[i, 0]; j++)
                shuffleList.Add(i);
        }
        currPos = shuffleList.Count - 1;
    }
    public void initilize()
    {
        createList();
    }

    public int getNext()//the next item in the list to get
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
        return temp;
    }
}
