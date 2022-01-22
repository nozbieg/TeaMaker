using System;
using System.Linq;

namespace TeaMaker;

public class TeaToBrew
{
    public string Name { get; set; }
    public string Fluid { get; set; }
    public int Temperature { get; set; }
    public int BrewTime { get; set; }
    public TeaToBrew(string[] input)
    {
        Name = input[0];
        Fluid = input[1];
        Temperature = int.Parse(input[2]);
        BrewTime = int.Parse(input[3]);
    }
    public TeaToBrew()
    {
    }
}
