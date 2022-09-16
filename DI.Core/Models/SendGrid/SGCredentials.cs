using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Core.Models.SendGrid;

public class SGCredentials
{
    public SGCredentials(string apiKey)
    {
        Apikey = apiKey;
    }
    public string Apikey { get; set; }
}
