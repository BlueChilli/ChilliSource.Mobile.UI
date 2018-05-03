#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Effect to display a line in an <see cref="Entry"/> underlining the text of the control
    /// </summary>
	public class EntryLineEffect : RoutingEffect
    {
        public Color LineColor { get; set; }

        public float LineHeight { get; set; }

        public EntryLineEffect() : this("ChilliSource.Mobile.UI.EntryLineEffect")
        {

        }

        public EntryLineEffect(string effectId) : base(effectId)
        {

        }
    }
}
