﻿using Sdl.Community.TMBackup.Models;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sdl.Community.TMBackup
{
	public class Persistence
	{
		private readonly string _persistancePath;

		public Persistence()
		{
			_persistancePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				@"SDL Community\TMBackup\TMBackup.json");
		}

		//private void CheckIfJsonFileExist(Object recoveredJson)
		//{
		//	if (!File.Exists(_persistancePath))
		//	{
		//		var directory = Path.GetDirectoryName(_persistancePath);
		//		if (directory != null && !Directory.Exists(directory))
		//		{
		//			Directory.CreateDirectory(directory);					
		//		}

		//		var json = JsonConvert.SerializeObject(recoveredJson);

		//		File.WriteAllText(_persistancePath, json);
		//	}
		//}

		private void CheckIfJsonFileExist()
		{
			if (!File.Exists(_persistancePath))
			{
				var directory = Path.GetDirectoryName(_persistancePath);
				if (directory != null && !Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
			}
		}

		public void SaveBackupFormInfo(BackupModel backupModel)
		{
			CheckIfJsonFileExist();

			var jsonText = File.ReadAllText(_persistancePath);
			var request = JsonConvert.DeserializeObject<BackupModel>(jsonText);

			request = backupModel;
			var json = JsonConvert.SerializeObject(request);

			File.WriteAllText(_persistancePath, json);
		}


		public void SaveDetailsFormInfo(List<BackupDetailsModel> backupDetailsModelList)
		{
			CheckIfJsonFileExist();

			var jsonText = File.ReadAllText(_persistancePath);
			var request = JsonConvert.DeserializeObject<List<BackupDetailsModel>>(jsonText);

			request = backupDetailsModelList;
			var json = JsonConvert.SerializeObject(request);

			File.WriteAllText(_persistancePath, json);
		}
	}
}