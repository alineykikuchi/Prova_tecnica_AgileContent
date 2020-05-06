using iTaas.Models.Extension;
using System.ComponentModel;

namespace iTaas.Models
{
    public enum EnumProcess
    {
        [DataValue("Y"), Description("Continue process creating")]
        yes = 1,

        [DataValue("N"), Description("Stop process for some modification")]
        no = 2,

        [DataValue("C"), Description("Abort process")]
        cancel = 3
    }
}
