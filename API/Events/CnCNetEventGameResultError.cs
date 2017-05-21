using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CnCNetEventGameResultError : EventArgs
{
    public string Response { get; private set; }

    public CnCNetEventGameResultError(string response)
    {
        Response = response;
    }
}
