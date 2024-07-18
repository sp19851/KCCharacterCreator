using CitizenFX.Core.Native;
using Core.Shared.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client
{
    public static class Configuration<T>
    {
        public static T Parse(string filePath)
        {

            var json = API.LoadResourceFile(API.GetCurrentResourceName(), filePath + ".json");

            //Logger.Error($"Configuration {json} ");


            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
