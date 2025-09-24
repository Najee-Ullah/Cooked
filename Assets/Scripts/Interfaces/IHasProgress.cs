using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressEventArgs> OnProgressMade;
    public class OnProgressEventArgs : EventArgs
    {
        public float currentProgress;
    }
}
