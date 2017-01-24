using System.ComponentModel.Composition;

using audatex.br.audabridge2.infrastructure.mef.interfaces;

namespace audatex.br.audabridge2.domain.plugin.t1.bradesco
{
    [Export(typeof(IPlugin))]
    public class T1BradescoPlugin : IPlugin
    {
        public object Execute(object i360Obj)
        {
            return i360Obj;
        }
    }
}
