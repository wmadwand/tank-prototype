using System.Collections;
using System.Collections.Generic;
using TestingTask.Combat;
using UnityEngine;

public class TargetCollection : MonoBehaviour
{
    public List<ITargetable> Targets { get; private set; }

    private void Awake()
    {
        Targets = new List<ITargetable>();
        //TODO: use MVP + enemy factory + object pool + HashTables for the cache, etc 
        GetComponentsInChildren(false, Targets);
    }

    public void Remove(ITargetable element)
    {
        Targets.Remove(element);
    }
}
