namespace Clibo.Core {
    using System.IO;
    using System.Text;

    public abstract class Clip {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Clippy.Core.Clippy"/> class.
        /// </summary>
        protected Clip()
        {
            this.accumulator = new StringBuilder();
        }
        
        /// <summary>
        /// Clears the remembered content.
        /// </summary>
        public void Clear()
        {
            this.accumulator.Clear();
        }
        
        /// <summary>
        /// Collect new text.
        /// </summary>
        public abstract void Collect();
        
        /// <summary>
        /// Appends a new line of text to the clipboard store.
        /// </summary>
        /// <param name="txt">A string with new text.</param>
        public void AppendLine(string txt)
        {
            string accText = this.accumulator.ToString().Trim();
            string[] accLines = accText.Split( '\n' );
            
            if ( accText != txt
              && accLines[ accLines.Length - 1 ] != txt )
            {
                this.accumulator.AppendLine( txt );
            }
            
            return;
        }
        
        /// <summary>
        /// Gets the text stored in the clipboard so far.
        /// </summary>
        /// <value>The text, as a string.</value>
        public string Text {
            get {
                return this.accumulator.ToString();
            }
            set {
                this.Clear();
                this.AppendLine( value );
            }
        }
        
        public static Clip Create()
        {
            Clip toret;
            System.PlatformID pid = System.Environment.OSVersion.Platform;
                
            // Sometimes MacOSX is reported as Unix instead of MacOSX.
            // Instead of platform check, check for Mac root folders
            if ( pid == System.PlatformID.Unix
              && Directory.Exists( "/Applications" )
              && Directory.Exists( "/System" )
              && Directory.Exists( "/Users" )
              && Directory.Exists( "/Volumes" ) )
            {
                pid = System.PlatformID.MacOSX;
            }
            
            if ( pid != System.PlatformID.MacOSX ) {
                toret = new Clippy();
            } else {
                toret = new Clipmac();
            }
            
            return toret;
        }
        
        private StringBuilder accumulator;
    }
}
