using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("dialogue")]  
public class DialogueViewer
{
    [XmlElement("node")] public Node[] nodes;

    public static DialogueViewer Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DialogueViewer));
        StringReader reader = new StringReader(_xml.text);
        return serializer.Deserialize(reader) as DialogueViewer;
    }
}

public class Node
{
    [XmlElement("name")] public string speakerName;
    [XmlElement("text")] public string text;
}