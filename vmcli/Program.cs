using System;
using Vcsos;
using DotArgs;
using System.Windows.Forms;

namespace vmcli
{
	class MainClass
	{
		internal static UInt32 GetValueFromArg(CommandLineArgs cmd, string val)
		{
			UInt32 outv;
			if (!UInt32.TryParse (cmd.GetValue<string> (val), out outv))
				throw  new ArgumentException ();

			return outv;
		}
		public static void Main (string[] args)
		{
			CommandLineArgs cmd = new CommandLineArgs();


			cmd.RegisterArgument( "i", new OptionArgument( null, true ) { HelpMessage="The image file." } );
			cmd.RegisterArgument( "r", new OptionArgument( "256" )  { HelpMessage="Ram size. [> 0]" });
			cmd.RegisterArgument( "t", new OptionArgument( "raw", true) { HelpMessage="Image type: gz,raw,deflate" } );
			cmd.RegisterArgument( "o", new OptionArgument( "console", false) { HelpMessage="Debug output" } );

			cmd.SetDefaultArgument( "i" );
			cmd.RegisterHelpArgument();

			if( !cmd.Validate(args) )
			{
				cmd.PrintHelp();
				return;
			}
			string imageFile;
			string debugFile;
			UInt32 ramSize;
			try
			{
				imageFile = cmd.GetValue<string>( "i" );
				ramSize = GetValueFromArg(cmd, "r" );
				debugFile = cmd.GetValue<string>("o");
			}
			catch {
				cmd.PrintHelp();
				return;
			}

			if (debugFile != "console") {
				Console.SetOut (new System.IO.StreamWriter (debugFile));
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			VM.Instance.CreateVM (ramSize);
			FramebufferForm form = new FramebufferForm ();

			byte[] data = InputFactory.GetInputClass (cmd).LoadFromFile (imageFile);

			if (!VM.Instance.Start (data)) {
				Console.WriteLine ("Not enough bytes to load this image.");
				return;
			}
			Application.Run (form);


		}
	}
}