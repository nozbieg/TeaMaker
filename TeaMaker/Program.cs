// See https://aka.ms/new-console-template for more information
Console.WriteLine("TeaMaker App v.1");

var teaMaker = new TeaMakerCore();

teaMaker.TeaList = await teaMaker.ImportFileToMemory("tea-data.txt");

teaMaker.DisplayMenu();

teaMaker.BrewOneTea(Console.ReadLine());
//teaMaker.DisplayMenu();