using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Laba7;
using System.Xml.Serialization;

namespace Laba7Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Cow cow = new Cow
            {
                Country = "USA",
                HideFromOtherAnimal = true,
                Name = "Bessie",
                WhatAnimal = "Cow"
            };

            // Сериализация в XML
            XmlSerializer serializer = new XmlSerializer(typeof(Cow));
            using (FileStream fs = new FileStream("Animal.xml", FileMode.Create))
            {
                serializer.Serialize(fs, cow);
            }

            Console.WriteLine("XML serialization completed.");
            
            // Получаем текущую сборку
            Assembly assembly = typeof(Animal).Assembly;

            // Создаем корневой элемент XML
            XElement rootElement = new XElement("AnimalLibrary");

            // Проходим по всем типам в сборке
            foreach (Type type in assembly.GetTypes())
            {
                // Проверяем, является ли тип классом и наследуется ли от Animal
                if (type.IsClass && type.IsSubclassOf(typeof(Animal)))
                {
                    // Создаем элемент для класса
                    XElement classElement = new XElement("Class", new XAttribute("Name", type.Name));

                    // Получаем атрибут CommentAttribute, если он есть
                    CommentAttribute commentAttribute = type.GetCustomAttribute<CommentAttribute>();
                    if (commentAttribute != null)
                    {
                        classElement.Add(new XElement("Comment", commentAttribute.Comment));
                    }

                    // Добавляем свойства класса
                    foreach (var property in type.GetProperties())
                    {
                        classElement.Add(new XElement("Property", new XAttribute("Name", property.Name), new XAttribute("Type", property.PropertyType.Name)));
                    }

                    // Добавляем методы класса
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    {
                        if (!method.IsSpecialName) // Исключаем свойства и конструкторы
                        {
                            classElement.Add(new XElement("Method", new XAttribute("Name", method.Name), new XAttribute("ReturnType", method.ReturnType.Name)));
                        }
                    }

                    // Добавляем класс в корневой элемент
                    rootElement.Add(classElement);
                }
            }

            // Сохраняем XML в файл
            XDocument xmlDocument = new XDocument(rootElement);
            xmlDocument.Save("AnimalLibrary.xml");

            Console.WriteLine("XML file generated successfully.");
            
        }
    }
}