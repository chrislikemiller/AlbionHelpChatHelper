using HelpChatHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HelpChatHelper.Util
{
    class EntryComparer : IComparer<EntryViewModel>
    {
        public int Compare([AllowNull] EntryViewModel x, [AllowNull] EntryViewModel y)
        {
            return x.ID.CompareTo(y.ID);
        }
    }
}
