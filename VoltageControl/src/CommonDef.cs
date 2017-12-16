using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoltageControl.CommonDEF
{
    enum RowState
    {
        ROW_STATE_NORMAL,
        ROW_STATE_SENDING,
        ROW_STATE_SENDDONE,
        ROW_STATE_TIMEOUT,
    }
}
