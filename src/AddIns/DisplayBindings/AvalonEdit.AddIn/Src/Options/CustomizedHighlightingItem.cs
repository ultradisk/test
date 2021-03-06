// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Windows.Media;

using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;

namespace ICSharpCode.AvalonEdit.AddIn.Options
{
	sealed class CustomizedHighlightingItem : IHighlightingItem
	{
		readonly IHighlightingItem original;
		readonly List<CustomizedHighlightingColor> customizationList;
		readonly string language;
		CustomizedHighlightingColor customization;
		
		public CustomizedHighlightingItem(List<CustomizedHighlightingColor> customizationList, IHighlightingItem original, string language,
		                                  bool canSetForeground = true, bool canSetBackground = true, bool canSetFont = true)
		{
			if (customizationList == null)
				throw new ArgumentNullException("customizationList");
			if (original == null)
				throw new ArgumentNullException("original");
			this.customizationList = customizationList;
			this.original = original;
			this.language = language;
			this.CanSetForeground = canSetForeground;
			this.CanSetBackground = canSetBackground;
			this.CanSetFont = canSetFont;
			foreach (CustomizedHighlightingColor c in customizationList) {
				if (c.Language == language && c.Name == this.Name) {
					this.customization = c;
					break;
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		void AllPropertiesChanged()
		{
			OnPropertyChanged("Bold");
			OnPropertyChanged("Italic");
			OnPropertyChanged("Underline");
			OnPropertyChanged("Foreground");
			OnPropertyChanged("UseDefaultForeground");
			OnPropertyChanged("Background");
			OnPropertyChanged("UseDefaultBackground");
			OnPropertyChanged("IsCustomized");
		}
		
		void SetCustomization(bool? bold = null, bool? italic = null, bool? underline = null,
		                      Color? foreground = null, bool? useDefaultForeground = null,
		                      Color? background = null, bool? useDefaultBackground = null)
		{
			CustomizedHighlightingColor newColor = new CustomizedHighlightingColor();
			newColor.Language = language;
			newColor.Name = this.Name;
			newColor.Bold = bold ?? this.Bold;
			newColor.Italic = italic ?? this.Italic;
			newColor.Underline = underline ?? this.Underline;
			
			if (useDefaultBackground ?? this.UseDefaultBackground)
				newColor.Background = null;
			else
				newColor.Background = background ?? this.Background;
			
			if (useDefaultForeground ?? this.UseDefaultForeground)
				newColor.Foreground = null;
			else
				newColor.Foreground = foreground ?? this.Foreground;
			
			// remove existing customization
			if (language == null)
				customizationList.RemoveAll(c => c.Name == this.Name);
			else if (customization != null)
				customizationList.Remove(customization);
			
			if (newColor.Bold == original.Bold && newColor.Italic == original.Italic && newColor.Underline == original.Underline &&
			    (newColor.Background == null) == original.UseDefaultBackground &&
			    (newColor.Background == null || newColor.Background == original.Background) &&
			    (newColor.Foreground == null) == original.UseDefaultForeground &&
			    (newColor.Foreground == null || newColor.Foreground == original.Foreground))
			{
				// all settings at default values, customization entry not necessary
				this.customization = null;
			} else {
				this.customization = newColor;
				// insert at beginning to ensure language-specific entries take preference over generic entries
				customizationList.Insert(0, newColor);
			}
			
			AllPropertiesChanged();
		}
		
		public string Name {
			get {
				return original.Name;
			}
		}
		
		public bool Bold {
			get {
				return (customization != null) ? customization.Bold : original.Bold;
			}
			set {
				SetCustomization(bold: value);
			}
		}
		
		public bool Italic {
			get {
				return (customization != null) ? customization.Italic : original.Italic;
			}
			set {
				SetCustomization(italic: value);
			}
		}
		
		public bool Underline {
			get {
				return (customization != null) ? customization.Underline : original.Underline;
			}
			set {
				SetCustomization(underline: value);
			}
		}
		
		public Color Foreground {
			get {
				return (customization != null) ? (customization.Foreground ?? original.Foreground) : original.Foreground;
			}
			set {
				SetCustomization(foreground: value, useDefaultForeground: (value == original.Foreground) ? default(bool?) : false);
			}
		}
		
		public bool UseDefaultForeground {
			get {
				return (customization != null) ? (customization.Foreground == null) : original.UseDefaultForeground;
			}
			set {
				SetCustomization(useDefaultForeground: value);
			}
		}
		
		public Color Background {
			get {
				return (customization != null) ? (customization.Background ?? original.Background) : original.Background;
			}
			set {
				SetCustomization(background: value, useDefaultBackground: (value == original.Background) ? default(bool?) : false);
			}
		}
		
		public bool UseDefaultBackground {
			get {
				return (customization != null) ? (customization.Background == null) : original.UseDefaultBackground;
			}
			set {
				SetCustomization(useDefaultBackground: value);
			}
		}
		
		public bool CanUseDefaultColors {
			get { return original.CanUseDefaultColors; }
		}
		
		public bool CanSetForeground { get; private set; }
		public bool CanSetBackground { get; private set; }
		public bool CanSetFont { get; private set; }
		
		public bool IsCustomized {
			get { return customization != null || original.IsCustomized; }
		}
		
		public void Reset()
		{
			original.Reset();
			SetCustomization(original.Bold, original.Italic, original.Underline, original.Foreground, original.UseDefaultForeground, original.Background, original.UseDefaultBackground);
			AllPropertiesChanged();
		}
		
		public IHighlightingDefinition ParentDefinition {
			get { return original.ParentDefinition; }
		}
		
		public void ShowExample(TextArea exampleTextArea)
		{
			original.ShowExample(exampleTextArea);
		}
		
		public override string ToString()
		{
			return this.Name;
		}
	}
}
