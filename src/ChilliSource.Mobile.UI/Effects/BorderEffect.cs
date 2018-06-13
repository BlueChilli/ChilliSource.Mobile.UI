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
    public enum BorderType
    {
        Solid,
        Dashed
    }

    /// <summary>
    /// Effect to customize the border of a view
    /// </summary>
	public class BorderEffect : RoutingEffect
    {
        public float BorderRadius { get; set; }

        public Color BorderColor { get; set; }

        public float BorderWidth { get; set; }

        public BorderType Type {get; set;}

            /// <summary>
        /// Gets or sets the width of the dash.
        /// </summary>
        /// <value>The width of the dash; the default is 4.</value>
        public int DashWidth { get; set; } = 8;

        /// <summary>
        /// Gets or sets the amount of the space between dashes.
        /// </summary>
        /// <value>The width of the space; the default is 4.</value>
        public int DashSpaceWidth { get; set; } = 4;

        public BorderEffect() : this("ChilliSource.Mobile.UI.BorderEffect")
        {

        }

        public BorderEffect(string effectId) : base(effectId)
        {

        }
    }
}
