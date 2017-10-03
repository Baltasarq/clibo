namespace Clibo.Core {
    using OS = System.Windows.Forms;
    
    public class Clippy: Clip {
        internal Clippy()
        {
        }
    
        /// <summary>
        /// Collect new text.
        /// </summary>
        public override void Collect()
        {
            if ( OS.Clipboard.ContainsText() ) {
                this.AppendLine( OS.Clipboard.GetText() );
                OS.Clipboard.Clear();
            }
        }
    }
}
