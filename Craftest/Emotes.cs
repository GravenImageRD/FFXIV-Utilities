using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace Craftest.Emotes
{
    internal class Emotes
    {
        List<string> EmoteList = new List<string>() { "dance", "cry", "laugh", "hum" };

        public List<KeyData> GetEmoteSequence(int index)
        {
            return EmoteList[index % EmoteList.Count].Select(x => new KeyData() { Code =});
        }
    }
}
