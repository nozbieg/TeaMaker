using System;
using System.Linq;

namespace TeaMaker;

public class TeaToBrew
{
    public string Name { get; set; }
    public int Temperature { get; set; }
    public int BrewTime { get; set; }
    public TeaToBrew(string[] input)
    {
        Name = input[0];
        Temperature = int.Parse(input[1]);
        BrewTime = int.Parse(input[2]);
    }
    public TeaToBrew()
    {
    }
}
