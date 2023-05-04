using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Block mazeBlock;
    public int size;

    private List<Block> blocksList = new List<Block>();

    private List<Block> currentBlocks = new List<Block>();
    private List<Block> completedBlocks = new List<Block>();


    // Start is called before the first frame update
    void Start()
    {
        MazeGenerate();
    }

    private void MazeGenerate()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos = new Vector3(i - (size / 2f), 0, j - (size / 2f));
                Block block_curr = Instantiate(mazeBlock, pos, Quaternion.identity, transform);
                blocksList.Add(block_curr);
            }
        }

        SelectNodeatRandom();
        CheckBlocks();
    }

    private void SelectNodeatRandom()
    {
        int index = Random.Range(0, blocksList.Count);
        currentBlocks.Add(blocksList[index]);
    }

    private void CheckBlocks()
    {
        while (completedBlocks.Count < blocksList.Count)
        {
            List<int> nextBlocks = new List<int>();
            List<int> nextDir = new List<int>();

            int blockIndex_current = blocksList.IndexOf(currentBlocks[currentBlocks.Count - 1]);
            int blockX = blockIndex_current / size;
            int blockY = blockIndex_current % size;

            if (blockX < size - 1)       //Right Dir -> 1
            {
                if (!completedBlocks.Contains(blocksList[blockIndex_current + size]) && !currentBlocks.Contains(blocksList[blockIndex_current + size]))
                {
                    nextDir.Add(1);
                    nextBlocks.Add(blockIndex_current + size);
                }
            }

            if (blockX > 0)          //Left Dir -> -1
            {
                if(!completedBlocks.Contains(blocksList[blockIndex_current - size]) && !currentBlocks.Contains(blocksList[blockIndex_current - size]))
                {
                    nextDir.Add(2);
                    nextBlocks.Add(blockIndex_current - size);
                }
            }

            if(blockY < size - 1)
            {
                if (!completedBlocks.Contains(blocksList[blockIndex_current + 1]) && !currentBlocks.Contains(blocksList[blockIndex_current + 1]))
                {
                    nextDir.Add(3);
                    nextBlocks.Add(blockIndex_current + 1);
                }
            }

            if (blockY > 0)
            {
                if (!completedBlocks.Contains(blocksList[blockIndex_current - 1]) && !currentBlocks.Contains(blocksList[blockIndex_current - 1]))
                {
                    nextDir.Add(4);
                    nextBlocks.Add(blockIndex_current - 1);
                }
            }


            if(nextDir.Count > 0)
            {
                int dir_current = Random.Range(0, nextDir.Count);
                Block block_current = blocksList[nextBlocks[dir_current]];

                switch(nextDir[dir_current])
                {
                    case 1:
                        block_current.Del_Walls(1);
                        currentBlocks[currentBlocks.Count - 1].Del_Walls(0);
                        break;

                    case 2:
                        block_current.Del_Walls(0);
                        currentBlocks[currentBlocks.Count - 1].Del_Walls(1);
                        break;

                    case 3:
                        block_current.Del_Walls(3);
                        currentBlocks[currentBlocks.Count - 1].Del_Walls(2);
                        break;

                    case 4:
                        block_current.Del_Walls(2);
                        currentBlocks[currentBlocks.Count - 1].Del_Walls(3);
                        break;
                }

                currentBlocks.Add(block_current);
            }
            else
            {
                completedBlocks.Add(currentBlocks[currentBlocks.Count - 1]);

                currentBlocks.RemoveAt(currentBlocks.Count - 1);
            }
        }
    }
}