using System;
using Vcsos;
using DotArgs;

namespace vmcli
{
	class MainClass
	{
		static UInt32 GetValueFromArg(CommandLineArgs cmd, string val)
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
			cmd.RegisterArgument( "s", new OptionArgument( "16", false )  { HelpMessage="The start adress from image. [> 0]" } );
			cmd.RegisterArgument( "r", new OptionArgument( "256" )  { HelpMessage="Ram size. [> 0]" });

			cmd.SetDefaultArgument( "i" );
			cmd.RegisterHelpArgument();

			if( !cmd.Validate(args) )
			{
				cmd.PrintHelp();
				return;
			}
			string imageFile;
			UInt32 startAdress, ramSize;
			try
			{
				imageFile = cmd.GetValue<string>( "i" );
				startAdress = GetValueFromArg(cmd, "s" );
				ramSize = GetValueFromArg(cmd, "r" );
			}
			catch {
				cmd.PrintHelp();
				return;
			}


			VM.Instance.CreateVM (ramSize);
			Console.WriteLine ("Image File: {0}:{2}\nRam: {1} Bytes\n" +
				"Press a key to start.", imageFile, ramSize,
				startAdress);
			Console.ReadKey (true);

			if (!VM.Instance.Start (imageFile, (int)startAdress)) {
				Console.WriteLine ("Not enough bytes to load this image.");
				return;
			}


			while (VM.Instance.IsAlive) {
			}
			string r = VM.Instance.Ram.ToString ();
			Console.WriteLine (r + System.Environment.NewLine);
			Console.WriteLine (VM.Instance.CPU.Register.ToString ());
		}
	}
}
