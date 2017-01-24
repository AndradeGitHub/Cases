using System.ComponentModel.Composition;

using audatex.br.audabridge2.infrastructure.mef.interfaces;

namespace audatex.br.audabridge2.domain.plugin.t3.bradesco
{
    [Export(typeof(IPlugin))]
    public class T3BradescoPlugin : IPlugin
    {
        public object Execute(object i360Obj)
        {
            return i360Obj;
        }
    }
}
