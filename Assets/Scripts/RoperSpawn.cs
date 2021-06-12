using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoperSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject partPrefab, parentObject;
    [SerializeField]
    int length = 1;
    [SerializeField]
    float partDistance = 0.21f;
    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;
    private void Update()
    {
        if (reset)
        {
            foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(tmp);
            }
        }
    }
    public void Spawn()
    {
        int count = (int)(length/partDistance); 
        for (int x = 0; x < count; x++)
        {
            GameObject tmp;
            tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance* (x+1)  , transform.position.z),Quaternion.identity, parentObject.transform );
            tmp.transform.eulerAngles = new Vector3(180, 0, 0);
            tmp.name = parentObject.transform.childCount.ToString();

            if (x == 0)
            {
                Destroy(tmp.GetComponent< CharacterJoint > ());
            }
            else
            {
                // recuper le le composant de jointure qui est connecter eu composant enfant
                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount -1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
