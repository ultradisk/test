﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ICSharpCode.FormsDesigner
{
	public sealed class FormKeyHandler : IMessageFilter
	{
		const int keyPressedMessage          = 0x100;
		const int leftMouseButtonDownMessage = 0x0202;
		
		readonly Dictionary<Keys, CommandWrapper> keyTable = new Dictionary<Keys, CommandWrapper>();
		FormsDesignerAppDomainHost host;
		public static bool inserted = false;
		
		public static void Insert(FormsDesignerAppDomainHost host)
		{
			inserted = true;
			Application.AddMessageFilter(new FormKeyHandler(host));
		}
		
		public FormKeyHandler(FormsDesignerAppDomainHost host)
		{
			this.host = host;
			
			// normal keys
			keyTable[Keys.Left]  = new CommandWrapper(MenuCommands.KeyMoveLeft);
			keyTable[Keys.Right] = new CommandWrapper(MenuCommands.KeyMoveRight);
			keyTable[Keys.Up]    = new CommandWrapper(MenuCommands.KeyMoveUp);
			keyTable[Keys.Down]  = new CommandWrapper(MenuCommands.KeyMoveDown);
			keyTable[Keys.Tab]   = new CommandWrapper(MenuCommands.KeySelectNext);
			keyTable[Keys.Delete]   = new CommandWrapper(MenuCommands.Delete);
			keyTable[Keys.Back]   = new CommandWrapper(MenuCommands.Delete);
			
			// shift modified keys
			keyTable[Keys.Left | Keys.Shift]  = new CommandWrapper(MenuCommands.KeySizeWidthDecrease);
			keyTable[Keys.Right | Keys.Shift] = new CommandWrapper(MenuCommands.KeySizeWidthIncrease);
			keyTable[Keys.Up | Keys.Shift]    = new CommandWrapper(MenuCommands.KeySizeHeightDecrease);
			keyTable[Keys.Down | Keys.Shift]  = new CommandWrapper(MenuCommands.KeySizeHeightIncrease);
			keyTable[Keys.Tab | Keys.Shift]   = new CommandWrapper(MenuCommands.KeySelectPrevious);
			keyTable[Keys.Delete| Keys.Shift]   = new CommandWrapper(MenuCommands.Delete);
			keyTable[Keys.Back| Keys.Shift]   = new CommandWrapper(MenuCommands.Delete);
			
			// ctrl modified keys
			keyTable[Keys.Left | Keys.Control]  = new CommandWrapper(MenuCommands.KeyNudgeLeft);
			keyTable[Keys.Right | Keys.Control] = new CommandWrapper(MenuCommands.KeyNudgeRight);
			keyTable[Keys.Up | Keys.Control]    = new CommandWrapper(MenuCommands.KeyNudgeUp);
			keyTable[Keys.Down | Keys.Control]  = new CommandWrapper(MenuCommands.KeyNudgeDown);
			
			// ctrl + shift modified keys
			keyTable[Keys.Left | Keys.Control | Keys.Shift]  = new CommandWrapper(MenuCommands.KeyNudgeWidthDecrease);
			keyTable[Keys.Right | Keys.Control | Keys.Shift] = new CommandWrapper(MenuCommands.KeyNudgeWidthIncrease);
			keyTable[Keys.Up | Keys.Control | Keys.Shift]    = new CommandWrapper(MenuCommands.KeyNudgeHeightDecrease);
			keyTable[Keys.Down | Keys.Control | Keys.Shift]  = new CommandWrapper(MenuCommands.KeyNudgeHeightIncrease);
		}
		
		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg != keyPressedMessage /*&& m.Msg != leftMouseButtonDownMessage*/) {
				return false;
			}
			
			IFormsDesigner formsDesigner = host.GetService(typeof(IFormsDesigner)) as IFormsDesigner;
			
			if (formsDesigner == null || (formsDesigner.DesignerContent != null && !((Control)formsDesigner.DesignerContent).ContainsFocus)) {
				return false;
			}
			
			Control originControl = Control.FromChildHandle(m.HWnd);
			if (originControl != null && formsDesigner.DesignerContent != null && formsDesigner.DesignerContent != originControl) {
				// Ignore if message origin not in forms designer
				// (e.g. navigating the main menu)
				return false;
			}
			
			Keys keyPressed = (Keys)m.WParam.ToInt32() | Control.ModifierKeys;
			
			if (keyPressed == Keys.Escape) {
				if (formsDesigner.IsTabOrderMode) {
					formsDesigner.HideTabOrder();
					return true;
				}
			}
			
			CommandWrapper commandWrapper;
			if (keyTable.TryGetValue(keyPressed, out commandWrapper)) {
				if (commandWrapper.CommandID == MenuCommands.Delete) {
					// Check Delete menu is enabled.
					if (!formsDesigner.EnableDelete) {
						return false;
					}
				}
				host.LoggingService.Debug("Run menu command: " + commandWrapper.CommandID);
				
				IMenuCommandService menuCommandService = host.MenuCommandService;
				ISelectionService   selectionService = (ISelectionService)host.GetService(typeof(ISelectionService));
				ICollection components = selectionService.GetSelectedComponents();
				if (components.Count == 1) {
					foreach (IComponent component in components) {
						if (HandleMenuCommand(component, keyPressed))
							return false;
					}
				}
				
				menuCommandService.GlobalInvoke(commandWrapper.CommandID);
				
				if (commandWrapper.RestoreSelection) {
					selectionService.SetSelectedComponents(components);
				}
				return true;
			}
			
			return false;
		}
		
		bool HandleMenuCommand(IComponent activeComponent, Keys keyPressed)
		{
			Assembly asm = typeof(WindowsFormsDesignerOptionService).Assembly;
			// Microsoft made ToolStripKeyboardHandlingService internal, so we need Reflection
			Type keyboardType = asm.GetType("System.Windows.Forms.Design.ToolStripKeyboardHandlingService");
			object keyboardService = host.GetService(keyboardType);
			if (keyboardService == null) {
				host.LoggingService.Debug("no ToolStripKeyboardHandlingService found");
				return false; // handle command normally
			}
			if (activeComponent is ToolStripItem) {
				if (keyPressed == Keys.Up) {
					keyboardType.InvokeMember("ProcessUpDown",
					                          BindingFlags.Instance
					                          | BindingFlags.Public
					                          | BindingFlags.InvokeMethod,
					                          null, keyboardService, new object[] { false });
					return true; // command was handled specially
				} else if (keyPressed == Keys.Down) {
					keyboardType.InvokeMember("ProcessUpDown",
					                          BindingFlags.Instance
					                          | BindingFlags.Public
					                          | BindingFlags.InvokeMethod,
					                          null, keyboardService, new object[] { true });
					return true; // command was handled specially
				}
			}
			bool active = (bool)keyboardType.InvokeMember("TemplateNodeActive",
			                                              BindingFlags.Instance
			                                              | BindingFlags.NonPublic
			                                              | BindingFlags.GetProperty,
			                                              null, keyboardService, null);
			if (active) {
				return true; // command will handled specially by the text box, don't invoke the CommandID
			}
			return false; // invoke the CommandID
		}
		
		sealed class CommandWrapper
		{
			CommandID commandID;
			bool      restoreSelection;
			
			public CommandID CommandID {
				get {
					return commandID;
				}
			}
			
			public bool RestoreSelection {
				get {
					return restoreSelection;
				}
			}
			
			public CommandWrapper(CommandID commandID) : this(commandID, false)
			{
			}
			public CommandWrapper(CommandID commandID, bool restoreSelection)
			{
				this.commandID        = commandID;
				this.restoreSelection = restoreSelection;
			}
		}
	}
}
