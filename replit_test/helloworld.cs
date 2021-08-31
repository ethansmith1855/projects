using System;

Console.WriteLine("Whats Your Name?");
string name = Console.ReadLine();
Console.Clear();
Console.WriteLine($"Hello {name}");
Console.ReadKey();
Console.Clear();
Console.WriteLine("How are you doing?");
string wellness = Console.ReadLine();
Console.Clear();
if (wellness == "good"){
  Console.WriteLine("Im glad to hear that " + name);
}
if (wellness == "bad"){
  Console.WriteLine("Im sorry to hear that " + name);
}
Console.ReadKey();

