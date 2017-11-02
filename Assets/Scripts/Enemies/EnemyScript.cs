using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyScript  {
    void GetAgents();
    void releasePlayer();
    void freePlayer();
    void releasePlayerNum(int playerNumberRelease);
   
}
