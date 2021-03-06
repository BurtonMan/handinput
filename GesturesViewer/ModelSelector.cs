﻿using System;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

using Common.Logging;

namespace GesturesViewer {
  class ModelSelector : INotifyPropertyChanged {
    static readonly ILog Log = LogManager.GetCurrentClassLogger();
    static readonly String ModelFilePattern = "*.mat";
    // File names that ends with time stamp.
    static readonly String TimeRegex = @"^\d{4}-\d{2}-\d{2}_\d{2}-\d{2}";

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<String> ModelFiles { get; private set; }
    public String SelectedModel {
      get {
        return selectedModel;
      }
      set {
        selectedModel = value;
        if (selectedModel != null)
          OnPropteryChanged("SelectedModel");
      }
    }

    String selectedModel, dir;

    public ModelSelector(String dir) {
      this.dir = dir;
      ModelFiles = new ObservableCollection<String>();
      Refresh();
    }

    public void Refresh() {
      Log.Debug("Refresh models."); 
      var files = Directory.GetFiles(dir, ModelFilePattern);
      ModelFiles.Clear();
      SelectedModel = null;
      for (var i = files.Length - 1; i >= 0; i--) {
        var f = files[i];
        ModelFiles.Add(f);
        var fileName = Path.GetFileName(f);
        if (SelectedModel == null && Regex.IsMatch(fileName, TimeRegex)) 
          SelectedModel = f;
      }
    }

    void OnPropteryChanged(String prop) {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
  }
}
