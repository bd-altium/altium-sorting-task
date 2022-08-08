using Sorter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorter.Comparers
{
    public class QueueItemComparer : IComparer<QueueItem>
    {
        public int Compare(QueueItem? first, QueueItem? second)
        {
            string? firstText = first?.Text;
            string? secondText = second?.Text;

            return new TextComparer().Compare(firstText, secondText);
        }
    }
}
