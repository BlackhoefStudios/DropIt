using System;
using Xamarin.Forms;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace DropIt.Controls
{
	public class BindablePicker : Picker
	{
		public BindablePicker()
		{
			this.SelectedIndexChanged += OnSelectedIndexChanged;
		}

		public static BindableProperty ItemsSourceProperty =
			BindableProperty.Create<BindablePicker, IEnumerable<object>>(o => o.ItemsSource, default(IEnumerable<object>), propertyChanged: OnItemsSourceChanged);

		public static BindableProperty SelectedItemProperty =
			BindableProperty.Create<BindablePicker, object>(o => o.SelectedItem, default(object),propertyChanged: OnSelectedItemChanged);

		public IEnumerable<object> ItemsSource
		{
			get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable<object> oldvalue, IEnumerable<object> newvalue)
		{
			var picker = bindable as BindablePicker;
			picker.Items.Clear();
			if (newvalue != null)
			{

				//now it works like "subscribe once" but you can improve
				foreach (var item in newvalue)
				{
					picker.Items.Add(item.ToString());
				}
			}
			picker.ItemsSource = newvalue;
		}

		private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
		{
			if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
			{
				SelectedItem = null;
			}
			else
			{
				SelectedItem = ItemsSource.ElementAt(SelectedIndex);
			}
		}

		private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var picker = bindable as BindablePicker;
			if (newvalue != null)
			{
				var index = -1;
				for (var i = 0; i < picker.ItemsSource.Count (); i++) {
					if (picker.ItemsSource.ElementAt(i).ToString() == newvalue.ToString()) {
						index = i;
						break;
					}
				}

				picker.SelectedIndex = index == -1 ? 0 : index;
			}
		}
	}
}