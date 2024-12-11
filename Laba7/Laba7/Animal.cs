using System;
using System.Xml.Serialization;//Laba8 

namespace Laba7
{
    [Serializable]//Laba8
    public abstract class Animal : IXmlSerializable //Laba8 IXmlSerializable
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public bool HideFromOtherAnimal { get; set; }
        public string WhatAnimal { get; set; }

        public abstract void SayHello();
        public abstract EClassificationAnimal GetClassificationAnimal();
        public abstract EFavoriteFood GetFavoriteFood();
        public void Deconstruct(out string country, out bool hideFromOtherAnimals, out string name, out string whatAnimal)
        {
            country = Country;
            hideFromOtherAnimals = HideFromOtherAnimal;
            name = Name;
            whatAnimal = WhatAnimal;

        }
//--------------------Laba8-------------------------------------------
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        
        public void ReadXml (System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            Country = reader.GetAttribute("Country");
            Name = reader.GetAttribute("Name");
            WhatAnimal = reader.GetAttribute("WhatAnimal");
            HideFromOtherAnimal = bool.Parse(reader.GetAttribute("HideFromOtherAnimal"));
            reader.ReadStartElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Country", Country);
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("WhatAnimal", WhatAnimal);
            writer.WriteAttributeString("HideFromOtherAnimal", HideFromOtherAnimal.ToString());

        }
//--------------------Laba8-------------------------------------------
    }
}
