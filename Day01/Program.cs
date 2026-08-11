string name = "Abdullah";
int age = 29;
string profession = "Software Engineer";
string[] favouriteLanguage = { "C#", "Dart" };

Console.WriteLine($"Hello {name}, you are {age} years old and you are a {profession}.");
Console.WriteLine($"Your favourite programming languages are {string.Join(", ", favouriteLanguage)}.");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();