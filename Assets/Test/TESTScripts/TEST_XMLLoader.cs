using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class TEST_XMLLoader : MonoBehaviour
{
    public Text id;
    public Text name;
    public Text effect;

    public Text page;
    public Text file;
    
    private int currIndex = 0;
    //Those node will load from file and show in inspector
    private Node[] node;
    //Current show node
    private Node currentNode;

    void Start()
    {
        LoadFile("Test_ItemData");
        file.text = Resources.Load<TextAsset>("Test_ItemData").text;
        if(node.Length > 0)
        {
            currentNode = node[currIndex];
            ShowNode(currentNode);
        }
    }

    //Set node
    private void LoadFile(string fileName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        //Load file and load xml
        xmlDoc.LoadXml(Resources.Load<TextAsset>(fileName).text);

        //Get the root(SingleNode)
        XmlNode rootNode = xmlDoc.SelectSingleNode("Root");
        //Get all child nodes from root
        XmlNodeList elements = rootNode.ChildNodes;
        
        //Malloc memery
        node = new Node[elements.Count];

        //Foreach the array, initialization all nodes
        for (int i = 0; i < node.Length; i++)
        {
            XmlElement xmlElement = (XmlElement)elements[i];
            //Set ID
            node[i].id = Convert.ToInt32(xmlElement.GetAttribute("ID"));

            //Set game data from root's child
            XmlNodeList childs = xmlElement.ChildNodes;
            node[i].name = childs.Item(0).InnerText;
            node[i].effect = childs.Item(1).InnerText;
        }
    }

    //Display one node to the screen
    private void ShowNode(Node newNode)
    {
        id.text = newNode.id.ToString();
        name.text = newNode.name;
        effect.text = newNode.effect;

        page.text = string.Format("{0}/{1}", currIndex + 1, node.Length);
    }

    //Change currentNode to the next
    public void Next()
    {
        currIndex++;
        currIndex = Mathf.Clamp(currIndex, 0, node.Length - 1);
        currentNode = node[currIndex];
        ShowNode(currentNode);
    }

    //Change currentNode to the prev
    public void Prev()
    {
        currIndex--;
        currIndex = Mathf.Clamp(currIndex, 0, node.Length - 1);
        currentNode = node[currIndex];
        ShowNode(currentNode);
    }
}

[Serializable]
public struct Node
{
    public int id;
    public string name;
    public string effect;
}
