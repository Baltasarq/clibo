namespace Clibo.Core {
    public class Clipmac: Clip {
        internal Clipmac()
        {
        }
        
        /// <summary>
        /// Collect new text.
        /// </summary>
        public override void Collect()
        {        
            this.AppendLine( MacClipboard.Paste() );
        }
    }
}
