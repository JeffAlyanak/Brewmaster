﻿using System;
using System.Windows.Forms;

namespace Brewmaster.Modules.Ppu
{
	public partial class SpriteViewer : UserControl
	{
		public SpriteViewer(Events events)
		{
			InitializeComponent();
			_spriteDisplay.ModuleEvents = events;
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			_spriteDisplay.Width = Width;
			_spriteDisplay.Height = Height - (_controlPanel.Visible ? _controlPanel.Height : 0) - 1;
			base.OnLayout(e);
		}
		public bool FitImage
		{
			get { return _scaleButton.Checked; }
			set { _scaleButton.Checked = value; }
		}

		private void _scaleButton_CheckedChanged(object sender, EventArgs e)
		{
			_spriteDisplay.FitImage = _scaleButton.Checked;
			// Removes confusing focus from current button
			//if (_scaleButton.Focused && _layerButtons.Count > _requestedLayerId) _layerButtons[_requestedLayerId].Focus();
		}
	}
}
