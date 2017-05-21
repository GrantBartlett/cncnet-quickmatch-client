using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class CnCNetEventUsernamesSuccess : EventArgs
{
    public List<string> Usernames { get; private set; }
    public JArray Response { get; private set; }

    public CnCNetEventUsernamesSuccess(string response)
    {
        Response = JArray.Parse(response);
        Usernames = getUsernamesWithList(Response);
    }

    private List<string> getUsernamesWithList(JArray response)
    {
        List<string> handles = new List<string>();
        JArray responseArray = response;

        foreach (JObject o in responseArray.Children<JObject>())
        {
            foreach (JProperty p in o.Properties())
            {
                string name = p.Name;
                string value = (string)p.Value;

                if (name == "username")
                {
                    handles.Add(value);
                }
            }
        }

        return handles;
    }
}
