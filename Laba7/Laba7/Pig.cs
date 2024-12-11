using System;

namespace Laba7
{
    [Comment("A domesticated mammal known for its omnivorous diet.")]
    public class Pig : Animal
    {
        public override void SayHello()
        {
            Console.WriteLine("Oink!");
        }

        public override EClassificationAnimal GetClassificationAnimal()
        {
            return EClassificationAnimal.Omnivores;
        }

        public override EFavoriteFood GetFavoriteFood()
        {
            return EFavoriteFood.Everything;
        }
    }
}