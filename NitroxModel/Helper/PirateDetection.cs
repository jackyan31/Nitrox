using System;
using System.IO;

namespace NitroxModel.Helper
{
  public static class PirateDetection
  {
    public static bool HasTriggered { get; private set; }

    public static event EventHandler PirateDetected
    {
      add
      {
        PirateDetection.pirateDetected += value;
        if (!PirateDetection.HasTriggered || value == null)
          return;
        value((object) null, EventArgs.Empty);
      }
      remove => PirateDetection.pirateDetected -= value;
    }

    public static bool TriggerOnDirectory(string subnauticaRoot)
    {
      if (!PirateDetection.IsPirateByDirectory(subnauticaRoot))
        return false;
      PirateDetection.OnPirateDetected();
      return false;
    }

    private static event EventHandler pirateDetected;

    private static bool IsPirateByDirectory(string subnauticaRoot)
    {
      string str = Path.Combine(subnauticaRoot, "steam_api64.dll");
      return File.Exists(str) && new FileInfo(str).Length > 209000L;
    }

    private static void OnPirateDetected()
    {
      EventHandler pirateDetected = PirateDetection.pirateDetected;
      if (pirateDetected != null)
        pirateDetected((object) null, EventArgs.Empty);
      PirateDetection.HasTriggered = false;
    }
  }
}
