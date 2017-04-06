using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class TEST_XMLWriter : MonoBehaviour
{
    [Header("Input Field Ref")]
    public InputField fileNameField;
    public InputField rootElementField;
    public InputField nodeNameField;
    public InputField attributeNameField;
    public InputField attributeValueField;
    public InputField contentNameField;
    public InputField contentField;

    [Header("Preview")]
    public Text preview;

    [Header("Message Box Obj")]
    public RectTransform messageBoxObj;
    public Text mesBoxTitle;
    public Text mesBoxText;

    [Header("Stats")]
    public Node currentNode;

    [Header("Other Lib")]
    public Text savePath;

    private string fileName = string.Empty;
    private string rootElementName = string.Empty;
    private List<Node> nodes = new List<Node>();
    private KeyAndValue currentAttribute = new KeyAndValue();
    private KeyAndValue currentContent = new KeyAndValue();

    void Start()
    {
        CheckFile();
        preview.text = ""; 
    }

    void Update()
    {
        savePath.text = HasError() ? "Save path: " + Application.dataPath + "/Data/XML File Save/" :                              "Save path: " + Application.dataPath + "/Data/XML File Save/" +                              fileName + ".xml";
        Preview(); 
    }

    private void CheckFile()
    {
        if (!Directory.Exists(Application.dataPath + "/Data"))
            Directory.CreateDirectory(Application.dataPath + "/Data");

        if (!Directory.Exists(Application.dataPath + "/Data/XML File Save/Temp"))
            Directory.CreateDirectory(Application.dataPath + "/Data/XML File Save/Temp");

        if (!Directory.Exists(Application.dataPath + "/Data/XML File Save"))
            Directory.CreateDirectory(Application.dataPath + "/Data/XML File Save");
    }

    private void Preview()
    {
        if(string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(rootElementName))
        {
            preview.text = "";
            return;
        }

        XmlWriter xmlWriter = XmlWriter.Create(Application.dataPath + "/Data/XML File Save/Temp/" + fileName + ".tmp");

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteWhitespace(Environment.NewLine);
        xmlWriter.WriteStartElement(rootElementName);
        xmlWriter.WriteWhitespace(Environment.NewLine);

        foreach (var item in nodes)
        {
            xmlWriter.WriteStartElement(item.name);
            foreach (var attribute_tmp in item.attributes)
                xmlWriter.WriteAttributeString(attribute_tmp.Key, attribute_tmp.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);
            
            foreach (var content_tmp in item.contents)
            {
                xmlWriter.WriteElementString(content_tmp.Key, content_tmp.Value);
                xmlWriter.WriteWhitespace(Environment.NewLine);
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteWhitespace(Environment.NewLine);
        }

        if(!string.IsNullOrEmpty(currentNode.name))
        {
            xmlWriter.WriteStartElement(currentNode.name);
            foreach (var item in currentNode.attributes)
                xmlWriter.WriteAttributeString(item.Key, item.Value);
            xmlWriter.WriteAttributeString(currentAttribute.Key, currentAttribute.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);

            foreach (var item in currentNode.contents)
            {
                xmlWriter.WriteElementString(item.Key, item.Value);
                xmlWriter.WriteWhitespace(Environment.NewLine);
            }
            xmlWriter.WriteElementString(currentContent.Key, currentContent.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);

            xmlWriter.WriteEndElement();
            xmlWriter.WriteWhitespace(Environment.NewLine);
        }

        xmlWriter.WriteEndElement();
        xmlWriter.WriteWhitespace(Environment.NewLine);
        xmlWriter.WriteEndDocument();

        xmlWriter.Flush();
        xmlWriter.Close();

        if (File.Exists(Application.dataPath + "/Data/XML File Save/Temp/" + fileName + ".tmp"))
            preview.text = File.ReadAllText(Application.dataPath + "/Data/XML File Save/Temp/" + fileName + ".tmp");

    }

    public void AddAttribute()
    {
        if(HasError(true))
        {
            MessageBox("ERROR", string.Format("<color=#0000a0ff>{0}</color> can not be emply!", GetFirstError(true)));
            return;
        }

        currentNode.attributes.Add(currentAttribute);
        currentAttribute = new KeyAndValue();

        attributeNameField.text = string.Empty;
        attributeValueField.text = string.Empty;
    }

    public void AddContent()
    {
        if (HasError())
        {
            MessageBox("ERROR", string.Format("<color=#0000a0ff>{0}</color> can not be emply!", GetFirstError()));
            return;
        }

        currentNode.contents.Add(currentContent);
        currentContent = new KeyAndValue();

        contentNameField.text = string.Empty;
        contentField.text = string.Empty;
    }

    public void AddNode()
    {
        if (HasError())
        {
            MessageBox("ERROR", string.Format("<color=#0000a0ff>{0}</color> can not be emply!", GetFirstError()));
            return;
        }
        if(!currentAttribute.IsAllNullOrEmpty)
            AddAttribute();

        if (!currentContent.IsAllNullOrEmpty)
            AddContent();

        nodes.Add(currentNode);
        currentNode = new Node();

        nodeNameField.text = string.Empty;
        attributeNameField.text = string.Empty;
        attributeValueField.text = string.Empty;
        contentNameField.text = string.Empty;
        contentField.text = string.Empty;

    }

    public void ClearCache()
    { }

    public void Save()
    {
        if (HasError())
        {
            MessageBox("ERROR", string.Format("<color=#0000a0ff>{0}</color> can not be emply!", GetFirstError()));
            return;
        }
        if (!File.Exists(Application.dataPath + "/Data/XML File Save/" + fileName + ".xml"))
            using (FileStream fs = File.Create(Application.dataPath + "/Data/XML File Save/" + fileName + ".xml"))
                fs.Close();
        else
        {
            File.Delete(Application.dataPath + "/Data/XML File Save/" + fileName + ".xml");
            using (FileStream fs = File.Create(Application.dataPath + "/Data/XML File Save/" + fileName + ".xml"))
                fs.Close();
        }

        XmlWriter xmlWriter = XmlWriter.Create(Application.dataPath + "/Data/XML File Save/" + fileName + ".xml");

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteWhitespace(Environment.NewLine);
        xmlWriter.WriteStartElement(rootElementName);
        xmlWriter.WriteWhitespace(Environment.NewLine);

        foreach (var item in nodes)
        {
            xmlWriter.WriteStartElement(item.name);
            foreach (var attribute_tmp in item.attributes)
                xmlWriter.WriteAttributeString(attribute_tmp.Key, attribute_tmp.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);

            foreach (var content_tmp in item.contents)
            {
                xmlWriter.WriteElementString(content_tmp.Key, content_tmp.Value);
                xmlWriter.WriteWhitespace(Environment.NewLine);
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteWhitespace(Environment.NewLine);
        }

        if (!string.IsNullOrEmpty(currentNode.name))
        {
            xmlWriter.WriteStartElement(currentNode.name);
            foreach (var item in currentNode.attributes)
                xmlWriter.WriteAttributeString(item.Key, item.Value);
            xmlWriter.WriteAttributeString(currentAttribute.Key, currentAttribute.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);

            foreach (var item in currentNode.contents)
            {
                xmlWriter.WriteElementString(item.Key, item.Value);
                xmlWriter.WriteWhitespace(Environment.NewLine);
            }
            xmlWriter.WriteElementString(currentContent.Key, currentContent.Value);
            xmlWriter.WriteWhitespace(Environment.NewLine);

            xmlWriter.WriteEndElement();
            xmlWriter.WriteWhitespace(Environment.NewLine);
        }

        xmlWriter.WriteEndElement();
        xmlWriter.WriteWhitespace(Environment.NewLine);
        xmlWriter.WriteEndDocument();

        xmlWriter.Flush();
        xmlWriter.Close();

        MessageBox("TIP", "Completed！");
    }

    private bool HasError(bool checkAttribute = false)
    {
        if (!checkAttribute)
            return string.IsNullOrEmpty(fileNameField.text)
                || string.IsNullOrEmpty(rootElementField.text);
        else
            return string.IsNullOrEmpty(fileNameField.text)
                || string.IsNullOrEmpty(rootElementField.text)
                || string.IsNullOrEmpty(currentAttribute.Key)
                || string.IsNullOrEmpty(currentAttribute.Value);
    }

    private string GetFirstError(bool checkAttribute = false)
    {
        if (string.IsNullOrEmpty(fileNameField.text))
            return "File Name";

        if (string.IsNullOrEmpty(rootElementField.text))
            return "Root Element Name";

        if(checkAttribute)
        {
            if (string.IsNullOrEmpty(currentAttribute.Key))
                return "Attribute Name";

            if (string.IsNullOrEmpty(currentAttribute.Value))
                return "Attribute Value";
        }

        if (string.IsNullOrEmpty(currentContent.Key))
            return "Content Name";

        if (string.IsNullOrEmpty(currentContent.Value))
            return "Content Value";

        return "\"\"";
    }

    public void MessageBox(string title, string text, bool show = true)
    {
        if (!messageBoxObj || !mesBoxTitle || !mesBoxText)
            return;

        mesBoxTitle.text = title;
        mesBoxText.text = text;

        messageBoxObj.gameObject.SetActive(show);
    }

    #region SetValue
    public void SetFileName()
    { fileName = fileNameField.text; }

    public void SetRootElementName()
    { rootElementName = rootElementField.text; }

    public void SetNodeName()
    { currentNode.name = nodeNameField.text; }

    public void SetAttributeName()
    { currentAttribute.Key = attributeNameField.text; }

    public void SetAttributeValue()
    { currentAttribute.Value = attributeValueField.text; }

    public void SetContentName()
    { currentContent.Key = contentNameField.text; }

    public void SetContentValue()
    { currentContent.Value = contentField.text; }

    #endregion

    public void ActiveTrigger(GameObject target)
    { target.SetActive(!target.activeSelf); }

    public void Quit()
    { Application.Quit(); }

    [Serializable]
    public class Node
    {
        public string name = string.Empty;
        public List<KeyAndValue> attributes = new List<KeyAndValue>();
        public List<KeyAndValue> contents = new List<KeyAndValue>();

        public void Clear()
        {
            name = string.Empty;
            attributes = new List<KeyAndValue>();
            contents = new List<KeyAndValue>();
        }
    }

    [Serializable]
    public class KeyAndValue
    {
        [SerializeField]
        private string key = string.Empty;
        [SerializeField]
        private string value = string.Empty;

        public string Key
        { 
            get { return key; }
            set { key = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public bool IsAllNullOrEmpty
        {
            get { return string.IsNullOrEmpty(key) && string.IsNullOrEmpty(value); }
        }

        public void Clear()
        {
            key = string.Empty;
            value = string.Empty;
        }
    }

}
