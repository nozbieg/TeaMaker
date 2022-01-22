// See https://aka.ms/new-console-template for more information
public class TeaParameter
{

    public string Name { get; set; }
    public string Type { get; set; }
    public int Temperature { get; set; }
    public int BrewTime { get; set; }
    public string? SpecialThings { get; set; }

    public TeaParameter(string[] importedLine)
    {
        Name = importedLine[0];
        Type = importedLine[1];
        Temperature = int.Parse(importedLine[2]);
        BrewTime = int.Parse(importedLine[3]) * 60;
        SpecialThings = importedLine[4];
    }
    public TeaParameter()
    {
    }
}