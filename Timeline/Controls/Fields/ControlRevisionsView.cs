﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using YL.Timeline.Controls.Behind;
using YL.Timeline.Controls.Fields.Commands;
using YL.Timeline.Controls.MainRegion;
using YL.Timeline.Entities;

namespace YL.Timeline.Controls.Fields
{
	public class ControlRevisionsView : ItemsControl
	{
		//private readonly Dictionary<Record, ControlRevisionView> _controlsCache = new Dictionary<Record, ControlRevisionView>();

		public static readonly DependencyProperty ControllerProperty = ControlTimeLine.ControllerProperty.AddOwner(typeof(ControlRevisionsView));

		public ViewportController Controller
		{
			get
			{
				return (ViewportController)GetValue(ControllerProperty);
			}
			set
			{
				SetValue(ControllerProperty, value);
			}
		}

		public static void SetController(UIElement element, ViewportController value)
		{
			element.SetValue(ControllerProperty, value);
		}
		public static ViewportController GetController(UIElement element)
		{
			return (ViewportController)element.GetValue(ControllerProperty);
		}

		public ControlRevisionsView()
		{
			VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
			var stackFactory = new FrameworkElementFactory(typeof(StackPanel));
			stackFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

			var factory = new FrameworkElementFactory(typeof(ScrollViewer));
			factory.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Visible);

			var ipFactory = new FrameworkElementFactory(typeof(ItemsPresenter));
			factory.AppendChild(ipFactory);

			var template = new ControlTemplate();
			template.VisualTree = factory;

			Template = template;

			ItemsPanel = new ItemsPanelTemplate(stackFactory);


			var itemFactory = new FrameworkElementFactory(typeof(ControlRevisionView));
			ItemTemplate = new DataTemplate { VisualTree = itemFactory };

			var menu = new ContextMenu();
			var onlyChecked = new MenuItem
			{
				Header = "Only changed",
				IsCheckable = true
			};
			onlyChecked.Command = new DelegateCommand(() =>
				{
					foreach (var v in Helpers.FindVisualChildrens<ControlRevisionView>(this))
					{
						v.OnlyChanged = onlyChecked.IsChecked;
					}
				});
			menu.Items.Add(onlyChecked);

			menu.Items.Add(new MenuItem
			{
				Header = "Close all details",
				Command = new DelegateCommand(() =>
				{
					var view = CollectionViewSource.GetDefaultView(ItemsSource);
					var list = (IList)view.SourceCollection;
					list.Clear();
				})
			});

			ContextMenu = menu;
		}

		/*protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);
			var hash = new HashSet<Record>(Items.OfType<Record>());
			foreach (var toRemove in _controlsCache.Where(kvp => !hash.Contains(kvp.Key)).ToList())
			{
				_controlsCache.Remove(toRemove.Key);
			}
		}*/

		/*protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			var record = item as Record;

			if (record == null)
			{
				base.PrepareContainerForItemOverride(element, item);
				return;
			}

			ControlRevisionView instance;
			if (!_controlsCache.TryGetValue(record, out instance))
			{
				instance = new ControlRevisionView();
				_controlsCache.Add(record, instance);
			}
			base.PrepareContainerForItemOverride(element, instance);
		}*/
	}
}
