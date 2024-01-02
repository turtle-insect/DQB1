using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQB1
{
	internal class Equipment : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly uint mAddress;

		public Equipment(uint address) => mAddress = address;

		public uint ID
		{
			get => SaveData.Instance().ReadNumber(mAddress, 2);
			set
			{
				SaveData.Instance().WriteNumber(mAddress, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}

		public uint Endurance
		{
			get => SaveData.Instance().ReadNumber(mAddress + 2, 2);
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 2, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Endurance)));
			}
		}

		
	}
}
