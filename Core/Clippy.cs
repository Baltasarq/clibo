namespace Clibo.Core {
    ﻿using System.Text;
    using OS = System.Windows.Forms;
    
    public class Clippy {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Clippy.Core.Clippy"/> class.
        /// </summary>
        public Clippy()
        {
            this.accumulator = new StringBuilder();
            this.Collect();
        }
        
        /// <summary>
        /// Collect new text.
        /// </summary>
        public void Collect()
        {
            if ( OS.Clipboard.ContainsText() ) {
                accumulator.AppendLine( OS.Clipboard.GetText() );
                OS.Clipboard.Clear();
            }
        }
        
        /// <summary>
        /// Clears the remembered content.
        /// </summary>
        public void Clear()
        {
            this.accumulator.Clear();
        }
        
        /// <summary>
        /// Gets the text stored in the clipboard so far.
        /// </summary>
        /// <value>The text, as a string.</value>
        public string Text {
            get {
                return this.accumulator.ToString();
            }
        }
        
        private StringBuilder accumulator;
    }
}
