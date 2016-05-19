using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAD.Services
{
public interface IQRCode
{
    Task<string> ScanAsync();
}
}
