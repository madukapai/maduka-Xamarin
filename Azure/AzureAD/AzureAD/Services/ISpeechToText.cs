using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureAD.Models;

namespace AzureAD.Services
{
    public interface ISpeechToText
    {
        Task<SpeechToTextResult> SpeechToTextAsync();
    }
}
