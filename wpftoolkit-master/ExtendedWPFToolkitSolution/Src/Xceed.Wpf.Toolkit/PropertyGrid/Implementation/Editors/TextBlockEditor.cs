/*************************************************************************************
   
   Toolkit for WPF

   Copyright (C) 2007-2019 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://github.com/xceedsoftware/wpftoolkit/blob/master/license.md

   For more features, controls, and fast professional support,
   pick up the Plus Edition at https://xceed.com/xceed-toolkit-plus-for-wpf/

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows.Controls;
using System.Windows;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
  public class TextBlockEditor : TypeEditor<TextBlock>
  {
    protected override TextBlock CreateEditor()
    {
      return new PropertyGridEditorTextBlock();
    }

    protected override void SetValueDependencyProperty()
    {
      ValueProperty = TextBlock.TextProperty;
    }

    protected override void SetControlProperties( PropertyItem propertyItem )
    {
      Editor.Margin = new System.Windows.Thickness( 5, 0, 0, 0 );
      Editor.TextTrimming = TextTrimming.CharacterEllipsis;
    }
  }

  public class PropertyGridEditorTextBlock : TextBlock
  {
    static PropertyGridEditorTextBlock()
    {
      DefaultStyleKeyProperty.OverrideMetadata( typeof( PropertyGridEditorTextBlock ), new FrameworkPropertyMetadata( typeof( PropertyGridEditorTextBlock ) ) );
    }
  }
}
