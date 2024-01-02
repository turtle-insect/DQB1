using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQB1
{
	internal class Player
	{
		public uint HP
		{
			get => SaveData.Instance().ReadNumber(0x1E368, 2);
			set => Util.WriteNumber(0x1E368, 2, 1, 999, value);
		}

		public uint MaxHP
		{
			get => SaveData.Instance().ReadNumber(0x1E366, 2);
			set => Util.WriteNumber(0x1E366, 2, 1, 999, value);
		}

		public uint Attack
		{
			get => SaveData.Instance().ReadNumber(0x1E36E, 2);
			set => Util.WriteNumber(0x1E36E, 2, 0, 999, value);
		}

		public uint Defense
		{
			get => SaveData.Instance().ReadNumber(0x1E370, 2);
			set => Util.WriteNumber(0x1E370, 2, 1, 999, value);
		}
	}
}
