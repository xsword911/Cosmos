using System;
using System.Linq;
using Cosmos.Assembler;
using Cosmos.Assembler.x86;

namespace Cosmos.Debug.DebugStub {
	public class Screen : Cosmos.Assembler.Code {
		public override void Assemble() {
			new Comment("X#: Group DebugStub");

			new Comment("X#: procedure Cls {");
			new Label("DebugStub_Cls");

			new Comment("X#: # VidBase");
			new Comment("VidBase");

			new Comment("X#: ESI = $B8000");
			new Mov{ DestinationReg = RegistersEnum.ESI, SourceValue = 0xB8000 };

			new Comment("X#: BeginLoop:");
			new Label("DebugStub_Cls_BeginLoop");

			new Comment("X#: # Text");
			new Comment("Text");

			new Comment("X#: AL = $00");
			new Mov{ DestinationReg = RegistersEnum.AL, SourceValue = 0x00 };

			new Comment("X#: ESI[0] = AL");
			new Mov{ DestinationReg = RegistersEnum.ESI, DestinationIsIndirect = true, DestinationDisplacement = 0, SourceReg = RegistersEnum.AL };

			new Comment("X#: ESI + 1");
			new INC { DestinationReg = RegistersEnum.ESI };

			new Comment("X#: # Colour");
			new Comment("Colour");

			new Comment("X#: AL = $0A");
			new Mov{ DestinationReg = RegistersEnum.AL, SourceValue = 0x0A };

			new Comment("X#: ESI[0] = AL");
			new Mov{ DestinationReg = RegistersEnum.ESI, DestinationIsIndirect = true, DestinationDisplacement = 0, SourceReg = RegistersEnum.AL };

			new Comment("X#: ESI + 1");
			new INC { DestinationReg = RegistersEnum.ESI };

			new Comment("X#: # End of Video Area");
			new Comment("End of Video Area");

			new Comment("X#: # VidBase + 25 * 80 * 2 = B8FA0");
			new Comment("VidBase + 25 * 80 * 2 = B8FA0");

			new Comment("X#: If (ESI < $B8FA0) goto BeginLoop");
			new Compare { DestinationReg = RegistersEnum.ESI, SourceValue = 0xB8FA0 };
			new ConditionalJump { Condition = ConditionalTestEnum.LessThan, DestinationLabel = "DebugStub_Cls_BeginLoop" };

			new Comment("X#: }");
			new Label("DebugStub_Cls_Exit");
			new Return();

			new Comment("X#: procedure DisplayWaitMsg2 {");
			new Label("DebugStub_DisplayWaitMsg2");

			new Comment("X#: # http://wiki.osdev.org/Text_UI");
			new Comment("http://wiki.osdev.org/Text_UI");

			new Comment("X#: # Later can cycle for x changes of second register:");
			new Comment("Later can cycle for x changes of second register:");

			new Comment("X#: # http://wiki.osdev.org/Time_And_Date");
			new Comment("http://wiki.osdev.org/Time_And_Date");

			new Comment("X#: #ESI = AddressOf("DebugWaitMsg")");
			new Comment("ESI = AddressOf("DebugWaitMsg")");

			new Comment("X#: # VidBase");
			new Comment("VidBase");

			new Comment("X#: EDI = $B8000");
			new Mov{ DestinationReg = RegistersEnum.EDI, SourceValue = 0xB8000 };

			new Comment("X#: # 10 lines down, 20 cols in (10 * 80 + 20) * 2)");
			new Comment("10 lines down, 20 cols in (10 * 80 + 20) * 2)");

			new Comment("X#: EDI + 1640");
			new Add { DestinationReg = RegistersEnum.EDI, SourceValue = 1640 };

			new Comment("X#: # Read and copy string till 0 terminator");
			new Comment("Read and copy string till 0 terminator");

			new Comment("X#: ReadChar:");
			new Label("DebugStub_DisplayWaitMsg2_ReadChar");

			new Comment("X#: AL = ESI[0]");
			new Mov{ DestinationReg = RegistersEnum.AL, SourceReg = RegistersEnum.ESI, SourceIsIndirect = true, SourceDisplacement = 0 };

			new Comment("X#: AL = 0");
			new Mov{ DestinationReg = RegistersEnum.AL, SourceValue = 0 };

			new Comment("X#: #JumpIf(Flags.Equal, "AfterMsg")");
			new Comment("JumpIf(Flags.Equal, "AfterMsg")");

			new Comment("X#: ESI + 1");
			new INC { DestinationReg = RegistersEnum.ESI };

			new Comment("X#: EDI[0] = AL");
			new Mov{ DestinationReg = RegistersEnum.EDI, DestinationIsIndirect = true, DestinationDisplacement = 0, SourceReg = RegistersEnum.AL };

			new Comment("X#: EDI + 2");
			new Add { DestinationReg = RegistersEnum.EDI, SourceValue = 2 };

			new Comment("X#: #Jump(".ReadChar")");
			new Comment("Jump(".ReadChar")");

			new Comment("X#: AfterMsg:");
			new Label("DebugStub_DisplayWaitMsg2_AfterMsg");

			new Comment("X#: }");
			new Label("DebugStub_DisplayWaitMsg2_Exit");
			new Return();

		}
	}
}
