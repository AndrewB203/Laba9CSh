using System;

namespace Laba7
{
    [Comment("A large carnivorous feline.")]
    public class Lion : Animal
    {
        public override void SayHello()
        {
            Console.WriteLine("Roar!");
        }

        public override EClassificationAnimal GetClassificationAnimal()
        {
            return EClassificationAnimal.Carnivores;
        }

        public override EFavoriteFood GetFavoriteFood()
        {
            return EFavoriteFood.Meat;
        }
    }
}