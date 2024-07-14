using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public GameObject Section;
    public GameObject Bridge;
    public GameObject Section2;
    public int Step = 5;
    private bool start;
    private GameObject[] SectionList;
    private GameObject[] BridgeList;
    private int[] RandomNumber;

    private void Start()
    {

        SectionList = new GameObject[Step + 1];
        BridgeList = new GameObject[Step+1];
        RandomNumber = new int[Step];
    }
    public void Delete()
    {
        for(int D = 0; D < Step+1; D++)
        {
            Destroy(SectionList[D]);
            Destroy(BridgeList[D]);

        }
        StartCoroutine(_Wait());
    }
    IEnumerator _Wait()
    {
        yield return new WaitForSeconds(0);
        SectionList[0] = Instantiate(Section, new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < Step; i++)
        {
            if (!start)
            {
                RandomNumber[i] = Random.Range(0, 4);
                start = true;
            }
            else
            {
                RandomNumber[i] = Random.Range(0, 3);
            }

            BridgeList[i] = Instantiate(Bridge, SectionList[i].GetComponent<Pos>().pos[RandomNumber[i]].GetComponent<Transform>().position, SectionList[i].GetComponent<Pos>().pos[RandomNumber[i]].GetComponent<Transform>().rotation.normalized);
            SectionList[i + 1] = Instantiate(Section2, BridgeList[i].GetComponent<Pos>().pos[1].GetComponent<Transform>().position, BridgeList[i].GetComponent<Pos>().pos[1].GetComponent<Transform>().rotation.normalized);


            for (int j = 0; j < Step; j++)
            {
                for (int k = 0; k < Step; k++)
                {
                    if (SectionList[j] != null & SectionList[k] != null)
                    {
                        if (SectionList[j].GetComponent<Pos>().pos[4].position == SectionList[k].GetComponent<Pos>().pos[4].position)
                        {
                            if (j != k)
                            {
                                if (j > k)
                                {
                                    Debug.Log(j + "And" + k);
                                    Destroy(SectionList[j]);
                                    Destroy(BridgeList[j - 1]);
                                    RandomNumber[i] = Random.Range(0, 3);
                                    BridgeList[j - 1] = Instantiate(Bridge, SectionList[j - 1].GetComponent<Pos>().pos[RandomNumber[j]].GetComponent<Transform>().position, SectionList[j - 1].GetComponent<Pos>().pos[RandomNumber[j]].GetComponent<Transform>().rotation.normalized);
                                    SectionList[j] = Instantiate(Section2, BridgeList[j - 1].GetComponent<Pos>().pos[1].GetComponent<Transform>().position, BridgeList[j - 1].GetComponent<Pos>().pos[1].GetComponent<Transform>().rotation.normalized);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
