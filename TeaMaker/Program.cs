// See https://aka.ms/new-console-template for more information
Console.WriteLine("TeaMaker App v.1");

var teaMaker = new TeaMakerCore();
teaMaker.TeaList = await teaMaker.ImportTeaParamsFileToMemory("tea-data.txt");
//teaMaker.TeaListToBrew = await teaMaker.ImportTeaToBrewFileToMemory("input-file.txt");
teaMaker.TouragToMake = await teaMaker.ImportTouragToMake("touareg-input.txt");
teaMaker.DisplayMenu();

teaMaker.BrewTourag();
//teaMaker.DisplayMenu();