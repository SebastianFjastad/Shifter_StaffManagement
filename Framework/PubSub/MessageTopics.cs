using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shifter.Domain
{
    public static class MessageTopics
    {
        public static string ShiftScheduleChanged = "ShiftScheduleChanged";

        public static string ShiftDeleted = "ShiftDeleted";

        public static string ShiftAssigned = "ShiftAssigned";
    }
}
