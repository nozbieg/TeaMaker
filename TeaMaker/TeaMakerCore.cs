// See https://aka.ms/new-console-template for more information
using TeaMaker;

public class TeaMakerCore
{
    public IList<TeaParameter> TeaList { get; set; }
    public IList<TeaToBrew> TeaListToBrew { get; set; }

    public void DisplayMenu()
    {
        ////Console.WriteLine("Witaj w aplikacji TeaMaker! Zaparzmy herbatę!");
        ////Console.WriteLine("Wybierz herbatę do zaparzenia:");
        //PrintTeaList();

        Console.WriteLine("Zaparzymy herbaty z pliku input-file");
        //Console.WriteLine("Naciśnij enter");
        //Console.ReadLine();
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

    public async void BrewOneTea(TeaToBrew selectedTea)
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

        await File.AppendAllTextAsync("result-5.txt", resString);

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