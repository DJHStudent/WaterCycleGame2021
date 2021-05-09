using System.Collections.Generic;
using UnityEngine;

public class ShuffleBag
{

    //based on this explanation https://gamedevelopment.tutsplus.com/tutorials/shuffle-bags-making-random-feel-more-random--gamedev-1249
    List<int> shuffleList = new List<int>(); //list of all elements wanting to pick from
    int currPos;
    bool isTypeSpawn = false;
    public void createList(int i)//the list of items created
    { //
        ObjStats[] plats = GameManager.levelGen.wall;
        LevelPacing[] pacing = GameManager.levelGen.levelPacing;

        for (int j = 0; j < pacing[i].platformSpawnChance.Length; j++)
        {
            for (int k = 0; k < pacing[i].platformSpawnChance[j]; k++)
                shuffleList.Add(j); //j == the popsition of the element in the wall;
        }
        currPos = shuffleList.Count - 1;
    }

    public void createTypes()//the list of when a specific pacing moment will occur
    {
        isTypeSpawn = true;
        LevelPacing[] pacing = GameManager.levelGen.levelPacing;
        for (int i = 0; i < pacing.Length; i++)
        {
            for (int j = 0; j < pacing[i].spawnChance; j++)
                shuffleList.Add(i);
        }
        currPos = shuffleList.Count - 1;
    }

    public int getNext()//the next item in the list to get
    {
        if(currPos < 0)
        {
            currPos = shuffleList.Count - 1;
            if (!isTypeSpawn)
                GameManager.levelGen.updateBag(); //add check so only updates the bag if not the pacingList

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