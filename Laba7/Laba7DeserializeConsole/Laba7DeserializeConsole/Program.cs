using System;
using System.IO;
using System.Xml.Serialization;
using Laba7;

namespace Laba7DeserializeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Десериализация из XML
            XmlSerializer serializer = new XmlSerializer(typeof(Cow));
            using (FileStream fs = new FileStream("AnimalLibrary.xml", FileMode.Open))
            {
                Cow cow = (Cow)serializer.Deserialize(fs);

                // Вывод на консоль
                Console.WriteLine($"Country: {cow.Country}");
                //Console.WriteLine($"HideFromOtherAnimals: {cow.HideFromOtherAnimals}");
                Console.WriteLine($"Name: {cow.Name}");
                Console.WriteLine($"WhatAnimal: {cow.WhatAnimal}");
            }
        }
    }
}