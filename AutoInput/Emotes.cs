using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using static InputSimulator.InputSimulator;

namespace Emotes
{
    internal static class Emotes
    {
        static List<string> EmoteList = new List<string>() { "DANCE", "CRY", "LAUGH", "HUM" };

        static public IEnumerable<KeyData> GetEmoteSequence(int index)
        {
            var Sequence = new List<KeyData>();
            Sequence.Add(new KeyData() { Code = VirtualKeyCode.RETURN });
            Sequence.Add(new KeyData() { Code = VirtualKeyCode.OEM_2 });
            foreach (char c in EmoteList[index % EmoteList.Count])
            {
                Debug.Write(c);
                Sequence.Add(new KeyData() { Code = (VirtualKeyCode)c });
            }
            Debug.WriteLine("");
            Sequence.Add(new KeyData() { Code = VirtualKeyCode.RETURN });
            return Sequence;
        }
    }
}
