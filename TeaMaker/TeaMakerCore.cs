// See https://aka.ms/new-console-template for more information
using TeaMaker;

public class TeaMakerCore
{
    public IList<TeaParameter> TeaList { get; set; }
    public IList<TeaToBrew> TeaListToBrew { get; set; }
    public TouragToMake TouragToMake { get; set; }

    public void DisplayMenu()
    {
        ////Console.WriteLine("Witaj w aplikacji TeaMaker! Zaparzmy herbatę!");
        ////Console.WriteLine("Wybierz herbatę do zaparzenia:");
        //PrintTeaList();

        Console.WriteLine("Zaparzymy herbaty z pliku input-file");
        //Console.WriteLine("Naciśnij enter");
        //Console.ReadLine();
    }

    public void BrewTourag()
    {
        var resultList = new List<string>();
        resultList.Add(BrewOneTea(TouragToMake.TeaBase));

        var teaParameters = TeaList.Where(x => x.Name == TouragToMake.Name).FirstOrDefault();
        if (teaParameters == null)
        {
            throw new Exception("Herbata z pliku input-file nie znajduje sie w pliku tea-data");
        }
        var teaVerification = new TeaVerification();
        resultList.Add(teaVerification.VerifyTea(teaParameters, TouragToMake));
        var touragResult = string.Empty;
        if (resultList.ElementAt(0) == resultList.ElementAt(1) && resultList.ElementAt(1) == "idealny")
        {
            touragResult = $"Chciałeś przygotowac Tourag na bazie {TouragToMake.TeaBase.Name} w temperaturze {TouragToMake.TeaBase.Temperature} stopni przez {TouragToMake.TeaBase.BrewTime} sekund\n" +
                $"która wyszła {resultList.ElementAt(0)}, natomiast sam Tourag parzyłeś w temperaturze {TouragToMake.Temperature} stopni, przez {TouragToMake.BrewTime} sekund i jest on {resultList.ElementAt(1)}";
        }
        else
        {
            touragResult = $"Chciałeś przygotowac Tourag na bazie {TouragToMake.TeaBase.Name} w temperaturze {TouragToMake.TeaBase.Temperature} stopni przez {TouragToMake.TeaBase.BrewTime} sekund\n" +
                $"która wyszła {resultList.ElementAt(0)}, natomiast sam Tourag parzyłeś w temperaturze {TouragToMake.Temperature} stopni, przez {TouragToMake.BrewTime} sekund i niestety Tourag jest okropny";
        }

        Console.WriteLine(touragResult);
        File.WriteAllText("result-6.txt", touragResult);
    }

    public async Task<TouragToMake> ImportTouragToMake(string inputFile)
    {

        var lines = await File.ReadAllLinesAsync(inputFile);
        var lineCounter = 0;
        var tourag = new TouragToMake();
        foreach (var line in lines)
        {
            if (!line.StartsWith('#') && line.Length > 0)
            {
                if (lineCounter % 2 == 0)
                {

                    tourag.TeaBase = new TeaToBrew(line.Split(','));
                }
                else
                {
                    var lineSplit = line.Split(',');
                    tourag.Name = lineSplit[0];
                    tourag.Fluid = lineSplit[1];
                    tourag.Temperature = int.Parse(lineSplit[2]);
                    tourag.BrewTime = int.Parse(lineSplit[3]);
                }
            }
            lineCounter++;
        }
        return tourag;
    }

    public void BrewTeasFromFile()
    {
        foreach (var teaToBrew in TeaListToBrew)
        {
            BrewOneTea(teaToBrew);
        }
    }
    public async Task<IList<TeaToBrew>> ImportTeaToBrewFileToMemory(string inputFile)
    {
        var tealist = new List<TeaToBrew>();
        var lines = await File.ReadAllLinesAsync(inputFile);
        foreach (var line in lines)
        {
            if (!line.StartsWith('#') && line.Length > 0)
            {
                tealist.Add(new TeaToBrew(line.Split(',')));
            }

        }
        return tealist;
    }

    public async void BrewOneTea(string? selectedTea)
    {
        int teanumber;
        var success = int.TryParse(selectedTea, out teanumber);
        if (!success)
        {
            Console.WriteLine("Wybór musi byc liczbą. Spróbój ponownie:");
            BrewOneTea(Console.ReadLine());
            return;
        }
        Console.WriteLine("Wprowadź temperaturę parzenia");
        var temperature = TakeParametere("temperatura");
        Console.WriteLine("Wprowadź czas parzenia");
        var brewTime = TakeParametere("czas parzenia");

        var teaVerification = new TeaVerification();
        var result = teaVerification.VerifyTea(TeaList[teanumber - 1], temperature, brewTime);
        var resString = $"Twoja herbata jest {result}.";
        Console.WriteLine(resString);

        await File.AppendAllTextAsync("result-4.txt", $"Wybrałeś {TeaList[teanumber - 1].Name}, parzyłeś ją w temperaturze {temperature} stopni, przez {brewTime} sekund.\n{resString}.");

    }

    public string BrewOneTea(TeaToBrew selectedTea)
    {
        var teaParameters = TeaList.Where(x => x.Name == selectedTea.Name).FirstOrDefault();
        if (teaParameters == null)
        {
            throw new Exception("Herbata z pliku input-file nie znajduje sie w pliku tea-data");
        }
        var teaVerification = new TeaVerification();
        var result = teaVerification.VerifyTea(teaParameters, selectedTea);
        var resString = $"Wybrałeś {selectedTea.Name}, parzyłeś ją w temperaturze {selectedTea.Temperature} stopni, przez {selectedTea.BrewTime} sekund.\nTwoja herbata jest{result}.\n\n";
        Console.WriteLine(resString);


        return result;

    }
    int TakeParametere(string paramName)
    {
        int parameter;
        var success = int.TryParse(Console.ReadLine(), out parameter);
        if (!success)
        {
            Console.WriteLine($"{paramName} musi być liczbą. Spróbój ponownie:");
            TakeParametere(paramName);
        }
        return parameter;
    }

    void PrintTeaList()
    {
        var counter = 1;
        foreach (var tea in TeaList)
        {
            Console.WriteLine($"{counter} - {tea.Name}");
            counter++;
        }
    }

    public async Task<List<TeaParameter>> ImportTeaParamsFileToMemory(string fileName)
    {
        var tealist = new List<TeaParameter>();
        var lines = await File.ReadAllLinesAsync(fileName);
        foreach (var line in lines)
        {
            if (!line.StartsWith('#') && line.Length > 0)
            {
                tealist.Add(new TeaParameter(line.Split(',')));
            }

        }

        return tealist;
    }
}