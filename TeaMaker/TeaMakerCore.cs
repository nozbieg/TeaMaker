// See https://aka.ms/new-console-template for more information
using TeaMaker;

public class TeaMakerCore
{
    public IList<TeaParameter> TeaList { get; set; }

    public void DisplayMenu()
    {
        Console.WriteLine("Witaj w aplikacji TeaMaker! Zaparzmy herbatę!");
        Console.WriteLine("Wybierz herbatę do zaparzenia:");
        PrintTeaList();
    }

    internal async void BrewOneTea(string? selectedTea)
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

    public async Task<IList<TeaParameter>> ImportFileToMemory(string fileName)
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