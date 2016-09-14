using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Fabric.EventManager.Instance.PostEvent("Music");
        Fabric.EventManager.Instance.SetParameter("Music", "Intensity2", 0, null);
        Fabric.EventManager.Instance.SetParameter("Music", "Intensity3", 0, null);
    }

    private void Update ()
    {
        Enemy[] enemies = GameUtils.GetAllEnemies();
        int enemyCount = enemies.Length;

        if (enemyCount == 0)
        {
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity2", 0, null);
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity3", 0, null);
        }
        else if(enemyCount < 10)
        {
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity2", 1, null);
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity3", 0, null);
        }
        else if(enemyCount < 30)
        {
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity2", 0, null);
            Fabric.EventManager.Instance.SetParameter("Music", "Intensity3", 1, null);
        }
    }
}
