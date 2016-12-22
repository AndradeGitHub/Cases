using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace abacanet.diamond.infrastructure.common
{
    public enum UserStatusEnum
    {
        [Description("Active")]
        ACTIVE = 1,
        [Description("Inactive")]
        INACTIVE = 2       
    }
}