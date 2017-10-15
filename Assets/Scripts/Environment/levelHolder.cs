using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelHolder : MonoBehaviour {
    int totalCollectedCogs;

    public void addCog()
    {
      //  print("Cog was added");
        totalCollectedCogs++;
    }

    public int getTotalCollected()
    {
      //  print("the nimber of cogs is" + totalCollectedCogs);
        return totalCollectedCogs;
    }
}
