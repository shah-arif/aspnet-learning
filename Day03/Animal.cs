
// class Animal
// {
//     public void Eat()
//     {
//         Console.WriteLine("Eating...");
//     }
// }

// class Dog : Animal
// {
//     public void Bark()
//     {
//         Console.WriteLine("Barking...");
//     }
// }


// Polymorphism

class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal Speaking...");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog Speaking...");
    }
}
