﻿/**************************************************************************************

   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  **************************************************************************************/


namespace Xceed.Wpf.Toolkit.LiveExplorer.Samples.Text.Views
{
  /// <summary>
  /// Interaction logic for WatermarkPasswordBoxView.xaml
  /// </summary>
  public partial class WatermarkPasswordBoxView : DemoView
  {
    public WatermarkPasswordBoxView()
    {
      InitializeComponent();
    }

    private void WatermarkPasswordBox_PasswordChanged( object sender, System.Windows.RoutedEventArgs e )
    {
      var passwordBox = sender as WatermarkPasswordBox;
      if( passwordBox != null )
      {
        _passwordTextBlock.Text = passwordBox.Password;
      }
    }
  }
}
