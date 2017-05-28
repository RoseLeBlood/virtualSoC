using System;
using Vcsos;
using DotArgs;

namespace vmcli
{
	class MainClass
	{
		internal static UInt32 GetValueFromArg(CommandLineArgs cmd, string val)
		{
			return (UInt32) RamCalc.Calc (cmd.GetValue<string> (val));
		}
		public static void Main (string[] args)
		{
			CommandLineArgs cmd = new CommandLineArgs();


			cmd.RegisterArgument( "i", new OptionArgument( "test.bin", true ) { HelpMessage="The image file." } );
			cmd.RegisterArgument( "r", new OptionArgument( "512b" )  { HelpMessage="Ram size. [> 0]" });
			cmd.RegisterArgument( "t", new OptionArgument( "raw", true) { HelpMessage="Image type: gz,raw" } );
			cmd.RegisterArgument( "o", new OptionArgument( "console", false) { HelpMessage="Debug output" } );
            cmd.RegisterArgument( "c", new OptionArgument( "2") { HelpMessage = "Number of CPU Cores" });

            cmd.SetDefaultArgument( "i" );
			cmd.RegisterHelpArgument();

			if( !cmd.Validate(args) )
			{
				cmd.PrintHelp();
				return;
			}
			string imageFile;
			string debugFile;
			UInt32 ramSize =10;
            uint numCores =  1;
			try
			{
				imageFile = cmd.GetValue<string>( "i" );
				ramSize = GetValueFromArg(cmd, "r" );
				debugFile = cmd.GetValue<string>("o");
                numCores = GetValueFromArg(cmd, "c");
			}
			catch {
				cmd.PrintHelp();
				return;
			}

			if (debugFile != "console") {
				Console.SetOut (new System.IO.StreamWriter (debugFile));
			}

			VM.Instance.CreateVM (ramSize, (int)numCores);
			FramebufferForm form = new FramebufferForm ();

			byte[] data = InputFactory.GetInputClass (cmd).LoadFromFile (imageFile);

			if (!VM.Instance.Start (data)) {
				Console.WriteLine ("Not enough bytes to load this image.");
				return;
			}
			form.Run ();
		}
	}
}