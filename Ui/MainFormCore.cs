namespace Clibo.Ui {
    using System.IO;
    using System.Windows.Forms;
    
    using Core;

    public partial class MainForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Clippy.Ui.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.clippy = new Clippy();
            this.Build();
            this.BuildTimer();
            
            this.FormClosed += (o, e) => Application.Exit();
            
            // Buttons at start
            this.tbbStart.Enabled = true;
            this.tbbStop.Enabled = false;
        }
        
        private void BuildTimer()
        {
            this.watchTimer = new Timer { Interval = 1000 };

            this.watchTimer.Tick += (o, e) => {
                clippy.Collect();
                
                this.edText.Text = clippy.Text;
                this.edText.SelectionStart = this.edText.TextLength;
                this.edText.ScrollToCaret();
            };
        }
        
        /// <summary>
        /// Executed when a toolbar button is pressed.
        /// </summary>
        /// <param name="bt">A <see cref="Button"/>.</param>
        private void OnToolbarButton(ToolBarButton bt)
        {
            switch( bt.Name ) {
                case "save":
                    this.Save();
                    break;
                case "copy":
                    this.Copy();
                    break;
                case "start":
                    this.Start();
                    break;
                case "stop":
                    this.Stop();
                    break;
                case "erase":
                    this.Erase();
                    break;
                case "about":
                    this.About();
                    break;
                default:
                    throw new System.ApplicationException( "no button" );
            }
            
            return;
        }
        
        /// <summary>
        /// Saves the text in a file.
        /// </summary>
        private void Save()
        {
            string text = this.edText.Text;
            
            this.tbbSave.Enabled = false;
            
            if ( !string.IsNullOrEmpty( text ) ) {
	            var fileDialog = new SaveFileDialog {
	                Filter = "txt|*.txt|All files|*.*"
	            };
	            
	            if ( fileDialog.ShowDialog() == DialogResult.OK  )
	            {
	                using (StreamWriter writer =
                                    File.CreateText( fileDialog.FileName ))
	                {
	                    try {
	                        writer.WriteLine( text );
	                    } catch(IOException exc)
	                    {
	                        MessageBox.Show(
	                            exc.Message,
	                            "ERROR",
	                            MessageBoxButtons.OK,
	                            MessageBoxIcon.Error );
	                    }
	                }
	                
	            }
            }
            
            this.tbbSave.Enabled = true;
            return;
        }
        
        /// <summary>
        /// Copies the whole editor to the clipboard.
        /// </summary>
        private void Copy()
        {
            string text = this.edText.Text;
            
            if ( !string.IsNullOrEmpty( text ) ) {
	            Clipboard.Clear();
	            Clipboard.SetText( text );
	            MessageBox.Show(
	                            "Everything copied to clipboard",
	                            "Info",
	                            MessageBoxButtons.OK,
	                            MessageBoxIcon.Information );
            }
            
            return;
        }
        
        /// <summary>
        /// Starts watching the clipboard.
        /// </summary>
        private void Start()
        {
            this.watchTimer.Start();
            
            this.tbbStart.Enabled = false;
            this.tbbStop.Enabled = true;
        }
        
        /// <summary>
        /// Stops watching the clipboard.
        /// </summary>
        private void Stop()
        {
            this.watchTimer.Stop();
            
            this.tbbStart.Enabled = true;
            this.tbbStop.Enabled = false;
        }
        
        /// <summary>
        /// Erase the whole contents.
        /// </summary>
        private void Erase()
        {
            this.clippy.Clear();
            this.edText.Clear();
        }
        
        /// <summary>
        /// Shows the "about" panel.
        /// </summary>
        private void About()
        {
            this.pnlAbout.Show();
        }
        
        private Timer watchTimer;
        private Clippy clippy;
    }
}
