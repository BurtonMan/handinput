﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Speech.Recognition;

using HandInput.Engine;

namespace GesturesViewer {
  partial class MainWindow {
    SpeechManager speechManager;

    void StartSpeechRecognition() {
      using (var memoryStream = new MemoryStream(
          Encoding.ASCII.GetBytes(Properties.Resources.SpeechGrammar))) {
        speechManager = SpeechManager.Create(kinectSensor.AudioSource.Start(), memoryStream);
        if (speechManager != null) {
          speechManager.SpeechRecognized += SpeechRecognized;
          speechManager.Start();
        }
      }
    }

    void SpeechRecognized(object sender, String s) {
      speechTextBox.Text = s;
      lock (inputServer)
        inputServer.Send(s);
    }

    void StopSpeechRecognition() {
      if (speechManager != null)
        speechManager.Stop();
    }

  }
}
