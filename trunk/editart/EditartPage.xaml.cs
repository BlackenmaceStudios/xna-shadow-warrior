using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using ImageExportLib.Internals;
using build;

namespace editart
{
    public partial class EditartPage : UserControl
    {
        Editart editart = new Editart();
        public static EventHandler saveGrpEvent;
        UserControl _parent;
        public EditartPage(UserControl parent)
        {
            InitializeComponent();
            _parent = parent;
            _parent.KeyDown += new KeyEventHandler(MainPage_KeyDown);
            _parent.KeyUp += new KeyEventHandler(MainPage_KeyUp);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            editart.Init(ref viewportimg);

            // Game loop.
            CompositionTarget.Rendering += new EventHandler(Page_CompositionTarget_Rendering);
        }

        void ExportTileClick(object sender, EventArgs e)
        {
            
            int width = Engine.tilesizx[editart.SelectedArtTile];
            int height = Engine.tilesizy[editart.SelectedArtTile];

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG File|*.png";
            dialog.FilterIndex = 1;

            if (width <= 0 || height <= 0 || Engine.waloff[editart.SelectedArtTile].memory == null)
            {
                MessageBox.Show("You can't export a tile that doesn't have any image data");
                return;
            }

            if (!dialog.ShowDialog().Value)
                return;

            byte[] imagedata = editart.ConvertTileToImage(editart.SelectedArtTile, width, height);

            System.IO.Stream file = dialog.OpenFile();
            System.IO.MemoryStream png = (System.IO.MemoryStream)PngEncoder.Encode(imagedata, width, height);

            file.Write( png.ToArray(), 0, (int)png.Length );
            file.Close();

            png.Dispose();
        }

        void ReplaceTileClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG File|*.png";
            dialog.FilterIndex = 1;

            if (dialog.ShowDialog().Value == false)
                return;

            System.IO.Stream fileStream = dialog.File.OpenRead();

            // This is kind of a long way to do this, but doing so we can properly load bmp and pngs,
            // without any extra code, basically we create a bitmapimage, than we create a WriteableBitmap,
            // we have to do it that way because writeablebitmap's constructor doesn't take one param,
            // and writeablebitmap is the only way to get the pixel data out of our bitmapimage.
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(fileStream);
            
            WriteableBitmap bitmapwrite = new WriteableBitmap(bitmap);
            if (bitmapwrite.PixelWidth <= 320 && bitmapwrite.PixelHeight <= 200)
            {
                editart.ReplaceCurrentTile(bitmapwrite.Pixels, bitmapwrite.PixelWidth, bitmapwrite.PixelHeight);
            }
            else
            {
                MessageBox.Show("Image size must be less than 320x200!");
            }

            // Close the opened stream.
            fileStream.Dispose();

            // Null out the bitmapmaps we had to create.
            bitmap = null;
            bitmapwrite = null;

            // Force the gc to reclaim any unused memory.
            GC.Collect();
        }

        void MainPage_KeyUp(object sender, KeyEventArgs e)
        {
            editart.editinputkeyup(e.Key);
            e.Handled = true;
        }

        void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            editart.editinputkey(e.Key);
            e.Handled = true;
        }

        void SaveTileToGrpClick(object sender, EventArgs e)
        {
            int tilefilenum = Engine.tilefilenum[editart.SelectedArtTile];
            int localtilestart = tilefilenum * 255;
            int localtileend = localtilestart + 255;
            
            string artfilename = "tiles000.art";
            artfilename = artfilename.Remove(5, 3);
            artfilename = artfilename.Insert(5, "" + (char)(((tilefilenum / 100) % 10) + 48) + "" + (char)(((tilefilenum / 10) % 10) + 48) + "" + (char)((tilefilenum % 10) + 48));

            kFileWrite fil = Engine.filesystem.OpenFileWrite(artfilename, 0);
            fil.io.Write((int)1); // art version.
            fil.io.Write((int)255); // num files - not used.
            fil.io.Write(localtilestart);
            fil.io.Write(localtileend);

            for (int i = localtilestart; i < localtileend + 1; i++)
            {
                fil.io.Write((short)Engine.tilesizx[i]);
            }

            for (int i = localtilestart; i < localtileend + 1; i++)
            {
                fil.io.Write((short)Engine.tilesizy[i]);
            }

            for (int i = localtilestart; i < localtileend + 1; i++)
            {
                fil.io.Write(Engine.picanm[i]);
            }

            for (int i = localtilestart; i < localtileend + 1; i++)
            {
                if (Engine.tilesizx[i] > 0 && Engine.tilesizy[i] > 0)
                {
                    if (Engine.waloff[i].memory == null)
                    {
                        Engine.loadtile((short)i);
                    }
                    fil.io.Write(Engine.waloff[i].memory);
                }
            }

            fil.Close();

            saveGrpEvent.Method.Invoke(saveGrpEvent.Target, new object[] { null, null });
        }

        void Page_CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (editart.DrawTileEditMenu && edittilemenu.Visibility == System.Windows.Visibility.Collapsed)
            {
                edittilemenu.Visibility = System.Windows.Visibility.Visible;
                viewportimg.Cursor = Cursors.Arrow;
            }
            else if (!editart.DrawTileEditMenu && edittilemenu.Visibility == System.Windows.Visibility.Visible)
            {
                edittilemenu.Visibility = System.Windows.Visibility.Collapsed;
                viewportimg.Cursor = Cursors.None;
            }
            editart.Frame();
        }
    }
}
