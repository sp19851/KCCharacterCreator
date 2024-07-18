using CitizenFX.Core.Native;
using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client
{
    public static class NUI
    {

        public static void RegisterNUICallback(string msg, Func<IDictionary<string, object>, CallbackDelegate, CallbackDelegate> callback)
        {
            API.RegisterNuiCallbackType(msg);            
            Main.Handlers[$"__cfx_nui:{msg}"] += new Action<ExpandoObject, CallbackDelegate>((body, resultCallback) => callback.Invoke(body, resultCallback));            
        }

        public static async void Execute(object obj)
        {
            await BaseScript.Delay(0);
            try
            {
                API.SendNuiMessage(JsonConvert.SerializeObject(obj));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NUI.Execute Error {ex.Message}");
                Debug.WriteLine($"obj {JsonConvert.SerializeObject(obj)}");
            }
        }

        public static async void FocusKeepInput(bool keep = true)
        {
            await BaseScript.Delay(0);
            API.SetNuiFocusKeepInput(keep);
        }


        public static async void Focus(bool cursor, bool focus)
        {
            await BaseScript.Delay(0);
            API.SetNuiFocus(focus, cursor);
        }

        public static async Task FadeOut(int duration = 1000)
        {

            API.DoScreenFadeOut(duration);

            while (!API.IsScreenFadedOut())
            {
                await BaseScript.Delay(0);
            }
            //API.SendNuiMessage(JsonConvert.SerializeObject(new { request = "logofade.show" }));
        }

        public static async Task FadeIn(int duration = 1000)
        {
            //API.SendNuiMessage(JsonConvert.SerializeObject(new { request = "logofade.hide" }));
            API.DoScreenFadeIn(duration);

            while (!API.IsScreenFadedIn())
            {
                await BaseScript.Delay(0);
            }

        }
    }
}
