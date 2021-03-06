

namespace TeaMaker;

class TeaVerification
{
    internal string VerifyTea(TeaParameter teaParameter, int temperature, int brewTime)
    {
        var minTeaTemp = teaParameter.Temperature - teaParameter.Temperature * 0.1;
        var maxTeaTemp = teaParameter.Temperature + teaParameter.Temperature * 0.1;
        var minBrewTime = teaParameter.BrewTime - teaParameter.BrewTime * 0.1;
        var maxBrewTime = teaParameter.BrewTime + teaParameter.BrewTime * 0.1;

        var errorList = new List<string>();

        if (temperature < minTeaTemp || brewTime < minBrewTime)
        {
            errorList.Add("słaba");
        }

        if (temperature > maxTeaTemp || brewTime > maxBrewTime)
        {
            errorList.Add("niesmaczna");
        }

        if (errorList.Count > 0)
        {
            return errorList.Last();
        }
        else
        {
            return "idealna";
        }
    }
    internal string VerifyTea(TeaParameter teaParameter, TeaToBrew teaToBrew)
    {
        var minTeaTemp = teaParameter.Temperature - teaParameter.Temperature * 0.1;
        var maxTeaTemp = teaParameter.Temperature + teaParameter.Temperature * 0.1;
        var minBrewTime = teaParameter.BrewTime - teaParameter.BrewTime * 0.1;
        var maxBrewTime = teaParameter.BrewTime + teaParameter.BrewTime * 0.1;

        var errorList = new List<string>();

        if (teaToBrew.Temperature < minTeaTemp || teaToBrew.BrewTime < minBrewTime)
        {
            errorList.Add("słaba");
        }

        if (teaToBrew.Temperature > maxTeaTemp || teaToBrew.BrewTime > maxBrewTime)
        {
            errorList.Add("niesmaczna");
        }

        if (errorList.Count > 0)
        {
            return errorList.Last();
        }
        else
        {
            return "idealna";
        }
    }
    public string VerifyTea(TeaParameter teaParameters, TouragToMake touragToMake)
    {
        var minTeaTemp = teaParameters.Temperature - teaParameters.Temperature * 0.1;
        var maxTeaTemp = teaParameters.Temperature + teaParameters.Temperature * 0.1;
        var minBrewTime = teaParameters.BrewTime - teaParameters.BrewTime * 0.1;
        var maxBrewTime = teaParameters.BrewTime + teaParameters.BrewTime * 0.1;

        var errorList = new List<string>();

        if (touragToMake.Temperature < minTeaTemp || touragToMake.BrewTime < minBrewTime)
        {
            errorList.Add("słaba");
        }

        if (touragToMake.Temperature > maxTeaTemp || touragToMake.BrewTime > maxBrewTime)
        {
            errorList.Add("niesmaczna");
        }

        if (errorList.Count > 0)
        {
            return errorList.Last();
        }
        else
        {
            return "idealna";
        }
    }
}
