namespace Clibo.Ui {
    using System.Drawing;
    using System.Diagnostics;
    using System.Windows.Forms;
    
    using Core;

    /// <summary>
    /// Main form.
    /// </summary>
    public partial class MainForm: Form {
        private void BuildTextBox()
        {
            this.edText = new TextBox{
                            Multiline = true,
                            Dock = DockStyle.Fill,
                            Font = new Font( FontFamily.GenericMonospace, 12 ),
                            WordWrap = true,
                            ScrollBars = ScrollBars.Vertical,
                            Padding = new Padding( 5 )
            };
        }
        
        private void BuildIcons()
        {
            try {
                this.clipIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.clip.png" ) );

                this.copyIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.copy.png" ) );

                this.saveIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.save.png" ) );

                this.startWatchIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.start_watch.png" ) );
                        
                this.stopWatchIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.stop_watch.png" ) );
                        
                this.eraseIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.erase.png" ) );
                        
                this.aboutIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                        GetManifestResourceStream( "Clibo.Res.about.png" ) );
            } catch(System.Exception e)
            {
                Debug.WriteLine( "ERROR loading icons: " + e.Message);
            }

            return;
        }
        
        private void BuildAboutPanel()
        {
            // Sizes
            Graphics grf = this.CreateGraphics();
            SizeF fontSize = grf.MeasureString( "M", this.defaultFont );
            int charSize = (int) fontSize.Width + 5;

            // Panel for about info
            this.pnlAbout = new Panel() {
                Dock = DockStyle.Bottom,
                BackColor = Color.LightYellow,
                ForeColor = Color.Black,
                Visible = false
            };
            this.pnlAbout.SuspendLayout();
            var lblAbout = new Label {
                Text = AppInfo.Name + " v" + AppInfo.Version + ", " + AppInfo.Author,
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font( this.defaultFont, FontStyle.Bold )
            };
            var btCloseAboutPanel = new Button() {
                Text = "X",
                Dock = DockStyle.Right,
                Width = charSize * 5,
                FlatStyle = FlatStyle.Flat,
                Font = new Font( this.defaultFont, FontStyle.Bold )
            };
            btCloseAboutPanel.FlatAppearance.BorderSize = 0;
            btCloseAboutPanel.Click += (o, evt) => this.pnlAbout.Hide();
            this.pnlAbout.Controls.Add( lblAbout );
            this.pnlAbout.Controls.Add( btCloseAboutPanel );
            this.pnlAbout.Hide();
            this.pnlAbout.MinimumSize = new Size( this.Width, lblAbout.Height + 5 );
            this.pnlAbout.MaximumSize = new Size( int.MaxValue, lblAbout.Height + 5 );

            this.pnlAbout.ResumeLayout( false );
        }

        private void BuildToolbar()
        {
            // Create image list
            var imgList = new ImageList{ ImageSize = new Size( 32, 32 ) };
            imgList.Images.AddRange( new Image[]{
                this.copyIcon, this.saveIcon,
                this.startWatchIcon, this.stopWatchIcon,
                this.eraseIcon, this.aboutIcon
            });
            
            // Toolbar
            this.tbBar = new ToolBar {
                Dock = DockStyle.Left,
                ImageList = imgList,
                ShowToolTips = true,
                BorderStyle = BorderStyle.None,
                Appearance = ToolBarAppearance.Flat
            };
            this.tbBar.SuspendLayout();

            // Buttons
            this.tbbCopy = new ToolBarButton {
                Name = "copy",
                ImageIndex = 0,
                ToolTipText = "copy all to clipboard"
            };
            this.tbbSave = new ToolBarButton {
                Name = "save",
                ImageIndex = 1,
                ToolTipText = "save a copy as..."
            };
            this.tbbStart = new ToolBarButton {
                Name = "start",
                ImageIndex = 2,
                ToolTipText = "start watching the clipboard"
            };
            this.tbbStop = new ToolBarButton {
                Name = "stop",
                ImageIndex = 3,
                ToolTipText = "stop watching the clipboard"
            };
            this.tbbErase = new ToolBarButton {
                Name = "erase",
                ImageIndex = 4,
                ToolTipText = "erases all contents"
            };
            this.tbbAbout = new ToolBarButton {
                Name = "about",
                ImageIndex = 5,
                ToolTipText = "about..."
            };

            // Triggers
            this.tbBar.ButtonClick += (object sender, ToolBarButtonClickEventArgs e)
                => this.OnToolbarButton( e.Button );

            // Polishing
            this.tbBar.Buttons.AddRange( new ToolBarButton[] {
                this.tbbCopy, this.tbbSave, this.tbbStart,
                this.tbbStop, this.tbbErase, this.tbbAbout
            });
            
            this.tbBar.ResumeLayout( true );
            this.tbBar.MaximumSize = new Size(
                                        (int) ( imgList.ImageSize.Width * 1.5 ),
                                        int.MaxValue );
        }
        
        private void Build()
        {
            this.defaultFont = new Font( SystemFonts.DefaultFont.FontFamily, 12 );
        
            this.SuspendLayout();
            this.BuildIcons();
            this.BuildTextBox();
            this.BuildToolbar();
            this.BuildAboutPanel();
            
            this.Text = AppInfo.Name;
            this.Icon = Icon.FromHandle( this.clipIcon.GetHicon() );
            
            this.Controls.Add( this.edText );
            this.Controls.Add( this.pnlAbout );
            this.Controls.Add( this.tbBar );
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size( 620, 460 );
            this.ResumeLayout( true );
        }
        
        private Panel pnlAbout;
        private Font defaultFont;
        
        private ToolBar tbBar;
        private ToolBarButton tbbCopy;
        private ToolBarButton tbbSave;
        private ToolBarButton tbbStart;
        private ToolBarButton tbbStop;
        private ToolBarButton tbbErase;
        private ToolBarButton tbbAbout;
        
        private TextBox edText;
        
        private Bitmap aboutIcon;
        private Bitmap copyIcon;
        private Bitmap clipIcon;
        private Bitmap saveIcon;
        private Bitmap eraseIcon;
        private Bitmap startWatchIcon;
        private Bitmap stopWatchIcon;
    }
}
