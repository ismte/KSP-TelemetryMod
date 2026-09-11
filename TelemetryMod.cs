using UnityEngine;
using KSP.UI.Screens;

namespace TelemetryMod
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class TelemetryMod : MonoBehaviour
    {
        private ApplicationLauncherButton appButton;
        private bool showWindow = false;
        private Rect windowRect = new Rect(20, 100, 240, 200);
        private Texture2D buttonTexture;

        private void Start()
        {
            if (GameDatabase.Instance.ExistsTexture("TelemetryMod/icon"))
            {
                buttonTexture = GameDatabase.Instance.GetTexture("TelemetryMod/icon", false);
            }
            else
            {
                buttonTexture = Texture2D.whiteTexture;
            }

            GameEvents.onGUIApplicationLauncherReady.Add(OnGUIAppLauncherReady);
        }

        private void OnDestroy()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIAppLauncherReady);
            if (appButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(appButton);
            }
        }

        private void OnGUIAppLauncherReady()
        {
            if (ApplicationLauncher.Instance != null && appButton == null)
            {
                appButton = ApplicationLauncher.Instance.AddModApplication(
                    ToggleWindow,
                    ToggleWindow,
                    null, null, null, null,
                    ApplicationLauncher.AppScenes.FLIGHT,
                    buttonTexture
                );
            }
        }

        private void ToggleWindow()
        {
            showWindow = !showWindow;
        }

        private void OnGUI()
        {
            if (showWindow)
            {
                windowRect = GUILayout.Window(
                    GetInstanceID(),
                    windowRect,
                    DrawWindow,
                    "Telemetri Paneli"
                );
            }
        }

        private void DrawWindow(int windowID)
        {
            Vessel activeVessel = FlightGlobals.ActiveVessel;

            if (activeVessel != null)
            {
                GUILayout.Label($"Hız: {activeVessel.speed:F1} m/s");
                GUILayout.Label($"İrtifa: {activeVessel.altitude:F0} m");

                // Güvenli Yakıt Taraması (Tüm parçaları gezerek LiquidFuel toplar)
                double currentFuel = 0;
                double maxFuel = 0;

                if (activeVessel.parts != null)
                {
                    foreach (Part p in activeVessel.parts)
                    {
                        foreach (PartResource r in p.Resources)
                        {
                            if (r.resourceName == "LiquidFuel")
                            {
                                currentFuel += r.amount;
                                maxFuel += r.maxAmount;
                            }
                        }
                    }
                }

                GUILayout.Label($"Yakıt: {currentFuel:F0} / {maxFuel:F0}");

                float scenePingMs = Time.smoothDeltaTime * 1000f;
                GUILayout.Label($"Gecikme: {scenePingMs:F0} ms");
            }
            else
            {
                GUILayout.Label("Aktif araç bulunamadı.");
            }

            GUI.DragWindow();
        }
    }
}