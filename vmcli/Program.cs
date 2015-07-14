using System;
using Vcsos;
using DotArgs;

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

			cmd.SetDefaultArgument( "i" );
			cmd.RegisterHelpArgument();

			if( !cmd.Validate(args) )
			{
				cmd.PrintHelp();
				return;
			}
			string imageFile;
			UInt32 ramSize;
			try
			{
				imageFile = cmd.GetValue<string>( "i" );
				ramSize = GetValueFromArg(cmd, "r" );
			}
			catch {
				cmd.PrintHelp();
				return;
			}


			VM.Instance.CreateVM (ramSize);
			FramebufferForm form = new FramebufferForm ();

			byte[] data = InputFactory.GetInputClass (cmd).LoadFromFile (imageFile);

			if (!VM.Instance.Start (data)) {
				Console.WriteLine ("Not enough bytes to load this image.");
				return;
			}
			System.Windows.Forms.Application.Run (form);

			while (VM.Instance.IsAlive) {
			}
			string r = VM.Instance.Ram.ToString ();
			Console.WriteLine (r + System.Environment.NewLine);
			Console.WriteLine (VM.Instance.CPU.L2.ToString ());
		}
	}
}