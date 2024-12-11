using System;


namespace Laba7
{
    [Comment("A domesticated mammal known for producing milk.")]
    public class Cow : Animal
    {
        public override void SayHello()
        {
            Console.WriteLine("Moo!");
        }

        public override EClassificationAnimal GetClassificationAnimal()
        {
            return EClassificationAnimal.Herbivores;
        }

        public override EFavoriteFood GetFavoriteFood()
        {
            return EFavoriteFood.Plants;
        }
    }
}