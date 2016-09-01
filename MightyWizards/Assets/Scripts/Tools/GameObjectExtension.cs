using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GameObjectExtension
{
    public static GameObject FindObjectWithTagIn(GameObject gameObjectToSearch, string tag)
    {
        if (gameObjectToSearch.CompareTag(tag))
            return gameObjectToSearch;

        foreach (Transform transform in gameObjectToSearch.transform)
            if (transform.gameObject.CompareTag(tag))
                return transform.gameObject;

        return null;
    }
}
