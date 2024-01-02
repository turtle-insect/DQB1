using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DQB1
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;


		public ICommand OpenFileCommand { get; private set; }
		public ICommand SaveFileCommand { get; private set; }
		public ICommand ImportFileCommand { get; private set; }
		public ICommand ExportFileCommand { get; private set; }
		public ICommand ChoiceItemCommand { get; private set; }
		public ICommand ChoiceEquipmentCommand { get; private set; }


		public Player Player { get; private set; } = new Player();
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Equipment> Equipments { get; private set; } = new ObservableCollection<Equipment>();

		public ObservableCollection<Item> RepositoryItems { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Equipment> RepositoryEquipments { get; private set; } = new ObservableCollection<Equipment>();

		public ViewModel()
		{
			OpenFileCommand = new CommandAction(OpenFile);
			SaveFileCommand = new CommandAction(SaveFile);
			ImportFileCommand	 = new CommandAction(ImportFile);
			ExportFileCommand = new CommandAction(ExportFile);
			ChoiceItemCommand = new CommandAction(ChoiceItem);
			ChoiceEquipmentCommand = new CommandAction(ChoiceEquipment);
		}

		private void Initialize()
		{
			Items.Clear();
			for(uint index = 0; index < 15; index++)
			{
				Items.Add(new Item(0x45020 + index * 4));
			}
			Equipments.Clear();
			for (uint index = 0; index < 16; index++)
			{
				Equipments.Add(new Equipment(0x4505C + index * 4));
			}
			RepositoryItems.Clear();
			for (uint index = 0; index < 192; index++)
			{
				RepositoryItems.Add(new Item(0x4509C + index * 4));
			}
			RepositoryEquipments.Clear();
			for (uint index = 0; index < 64; index++)
			{
				RepositoryEquipments.Add(new Equipment(0x4539C + index * 4));
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Player)));
		}

		private void OpenFile(Object? parameter)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName);
			Initialize();
		}

		private void SaveFile(Object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void ImportFile(Object? parameter)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Import(dlg.FileName);
		}

		private void ExportFile(Object? parameter)
		{
			var dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}

		private void ChoiceItem(Object? parameter)
		{
			if (parameter is not Item item) return;

			var dlg = new ChoiceWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();

			item.ID = dlg.ID;
			if (item.Count == 0) item.Count = 1;
		}

		private void ChoiceEquipment(Object? parameter)
		{
			if (parameter is not Equipment item) return;

			var dlg = new ChoiceWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();

			item.ID = dlg.ID;
			if (item.Endurance == 0) item.Endurance = 1;
		}
	}
}
