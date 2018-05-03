using System;
using System.Collections.Generic;
using System.Text;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    public static class Ensure
    {
        public static void ArgumentNotNull(object source, string argumentName)
        {
            if (source == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
