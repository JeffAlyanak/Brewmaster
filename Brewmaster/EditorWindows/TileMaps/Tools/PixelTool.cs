﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using Brewmaster.Modules.Ppu;

namespace Brewmaster.EditorWindows.TileMaps.Tools
{
	public abstract class PixelTool : MapEditorTool
	{
		private Bitmap _pixelImage;
		private Palette _previewSource;
		private int _selectedColor;

		protected readonly TileMap _map;
		protected readonly MapEditorState _state;
		public event Action PreviewSourceChanged;
		public event Action SelectedColorChanged;

		public PixelTool(MapEditorState state, TileMap map)
		{
			_state = state;
			_map = map;
			Size = new Size(1, 1);
			Image = _pixelImage = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
			PreviewSource = map.Palettes[0];
		}
		public override bool Pixel { get { return true; } }
		public override bool EditsChr { get { return true; } }
		public bool CreateNewTile { get; set; }

		public override void EyeDrop(int x, int y, TileMapScreen screen)
		{
			var tile = screen.GetTile(x / _map.BaseTileSize.Width, y / _map.BaseTileSize.Height);
			SelectedColor = _state.GetPixel(tile, x % _map.BaseTileSize.Width, y % _map.BaseTileSize.Height);
		}

		public override void RefreshImage(Palette palette)
		{
			_pixelImage.SetPixel(0, 0, palette.Colors[SelectedColor]);
		}

		public override void AfterPaint()
		{
			_state.OnChrDataChanged();
		}

		public int SelectedColor
		{
			get { return _selectedColor; }
			set
			{
				_selectedColor = value;
				if (SelectedColorChanged != null) SelectedColorChanged();
			}
		}

		public Palette PreviewSource
		{
			get { return _previewSource; }
			set
			{
				_previewSource = value;
				if (PreviewSourceChanged != null) PreviewSourceChanged();
			}
		}

	}
}