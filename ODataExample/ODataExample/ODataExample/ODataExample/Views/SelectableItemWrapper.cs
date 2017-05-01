using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace ODataExample
{
	public class SelectableItemWrapper<T>
	{
		public bool IsSelected { get; set; }
		public T Item { get; set; }
	}
}
