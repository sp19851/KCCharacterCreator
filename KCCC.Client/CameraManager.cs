using CitizenFX.Core.Native;
using Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client
{
    public static class CameraManager
    {
        private static List<CameraModel> cams = new List<CameraModel>();

        public static CameraModel GetActiveCam() => cams.FirstOrDefault(x => x.IsActive);
        
            
        
        public static CameraModel Get(CameraModel cam) => cams.Find(x => x == cam);
        public static bool Exists(CameraModel cam) => cams.Exists(x => x == cam);
        public static void SwitchCameraToPlayer()
        {
            API.RenderScriptCams(false, false, 2000, false, false);
        }
        public static async Task<CameraModel> CreateCam(string name, float x, float y, float z, float rotX, float rotY, float rotZ, float fov, Action<CameraModel> action, string scene = "")
        {
            var cam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", x, y, z, rotX, rotY, rotZ, fov, false, 0);
            var newpos = new CamPos(x, y, z, rotX, rotY, rotZ, fov);
            var _cam = new CameraModel(name, newpos, action);
            _cam.Handle = cam;
            _cam.Scene = scene;
            await Delay(0);
            cams.Add(_cam);
            return _cam;
        }

        public static async Task<CameraModel> CreateCam(string name, float x, float y, float z, float rotX, float rotY, float rotZ, float fov, string scene = "")
        {
            var cam = API.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", x, y, z, rotX, rotY, rotZ, fov, false, 0);
            var newpos = new CamPos(x, y, z, rotX, rotY, rotZ, fov);
            var _cam = new CameraModel(name, newpos);
            if (cam == -1) return null;
            _cam.Handle = cam;
            _cam.Scene = scene;
            await Delay(0);

            cams.Add(_cam);

            return _cam;
        }

        private static async Task Delay(int v)
        {
            await Task.Delay(v);
        }

        public static Task DeleteScene(string sceneName)
        {
            for (int i = cams.Count - 1; i >= 0; i--)
            {
                if (cams[i].Scene == sceneName)
                {
                    DeActivateCam(cams[i]);
                    API.DestroyCam(cams[i].Handle, true);
                    cams.Remove(cams[i]);
                    
                }
            }
            return Task.FromResult(0);
        }

        public static void ActivateCam(CameraModel cam)
        {
            if (cam == null) return;
            foreach (var _cam in cams)
            {
                DeActivateCam(_cam);
            }
            if (cam.Handle == -1) return;
            API.SetCamActive(cam.Handle, true);
            API.RenderScriptCams(true, true, 1000, true, true);
            cam.IsActive = true;
        }

        public static void DeActivateCam(CameraModel cam)
        {
            if (cam == null) return;
            API.SetCamActive(cam.Handle, false);
            API.RenderScriptCams(true, true, 1000, true, true);
            cam.IsActive = false;
        }
        public static void Delete(CameraModel cam)
        {
            if (cam == null) return;
            if (Exists(cam))
            {
                var _cam = Get(cam).Handle;
                if (API.DoesCamExist(_cam)) API.DestroyCam(_cam, true);

                cams.Remove(Get(cam));
            }
        }
        
    }
}
