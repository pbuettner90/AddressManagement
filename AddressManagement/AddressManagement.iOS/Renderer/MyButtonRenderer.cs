using System;
using AddressManagement.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(MyButtonRenderer))]

namespace AddressManagement.iOS
{
	public class MyButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.TitleLabel.LineBreakMode = UIKit.UILineBreakMode.WordWrap;
			}
		}
	}
}
