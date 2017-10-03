namespace Clibo.Core {
    using System.Diagnostics;

    /// <summary>
    /// Mac clipboard.
    /// This class is needed since the standard winforms clipboard
    /// does not work on Mac. Go figure.
    /// </summary>
	public static class MacClipboard {
	    /// <summary>
	    /// Copy on MAC OS X using pbcopy commandline
	    /// </summary>
	    /// <param name="textToCopy"></param>
	    public static void Copy(string textToCopy)
	    {
	        try {
	            using ( var p = new Process() ) {
                    p.StartInfo = new ProcessStartInfo (
                                    "pbcopy",
                                    "-pboard general -Prefer txt" )
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        RedirectStandardInput = true
                    };
                    
                    p.Start();
	                p.StandardInput.Write( textToCopy );
	                p.StandardInput.Close();
	                p.WaitForExit();
	            }
	        }
	        catch (System.Exception exc)
	        {
	            Trace.WriteLine(exc.Message);
	        }
	
	
	    }
	
	    /// <summary>
	    /// Paste on MAC OS X using pbpaste commandline
	    /// </summary>
	    /// <returns></returns>
	    public static string Paste()
	    {
	        try {
	            string pasteText;
                
	            using ( var p = new Process() ) {

                    p.StartInfo = new ProcessStartInfo(
                                    "pbpaste",
                                    "-pboard general" )
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };
                    
                    p.Start();
	                pasteText = p.StandardOutput.ReadToEnd();
	                p.WaitForExit();
	            }
	
	            return pasteText;
	        }
	        catch (System.Exception ex)
	        {
	            Trace.WriteLine(ex.Message);
	            return "";
	        }
	
	    }
	}
}
