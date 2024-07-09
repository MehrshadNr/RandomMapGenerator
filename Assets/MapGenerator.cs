using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public GameObject Section;
    public GameObject Section2;
    private Pos SectionPos;
    private int RandomNumber;
    public int Step = 5;
    public GameObject[] SectionList;
    public GameObject[] BridgeList;
    public bool start;
    public bool Break;
    public GameObject Bridge;

    private void Start()
    {
        SectionPos = Section.GetComponent<Pos>();

        SectionList = new GameObject[Step + 1];
        BridgeList = new GameObject[Step];

        SectionList[0] = Instantiate(Section, new Vector3(0,0,0), Quaternion.identity);

        for (int i = 0; i < Step; i++)
        {
            if (!start)
            {
                RandomNumber = Random.Range(0, 4);
                start = true;
            }
            else
            {
                RandomNumber = Random.Range(0, 3);
            }

            Debug.Log(RandomNumber);
            BridgeList[i] = Instantiate(Bridge, SectionList[i].GetComponent<Pos>().pos[RandomNumber].GetComponent<Transform>().position, SectionList[i].GetComponent<Pos>().pos[RandomNumber].GetComponent<Transform>().rotation.normalized);
            SectionList[i + 1] = Instantiate(Section2, BridgeList[i].GetComponent<Pos>().pos[1].GetComponent<Transform>().position, BridgeList[i].GetComponent<Pos>().pos[1].GetComponent<Transform>().rotation.normalized);
            //Break = false;
            for (int j = 0; j < Step; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        if (SectionList[j] != null)
                        {
                            if (SectionList[i].GetComponent<Pos>().pos[k].position == SectionList[j].GetComponent<Pos>().pos[l].position)
                            {
                                if (i != j)
                                {
                                    Debug.Log(i + "+" + j);
                                    Destroy(SectionList[i]);
                                    Destroy(BridgeList[i - 1]);
                                    Break = false;
                                    //i -= 1;
                                    //break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
