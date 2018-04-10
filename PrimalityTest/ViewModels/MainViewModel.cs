using System;
using System.Numerics;
using System.Windows.Input;

namespace PrimalityTest.ViewModels
{
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using Utils;
    class MainViewModel : ViewModelBase
    {
        #region Parameters

        /// <summary>
        /// Title of the application, as displayed in the top bar of the window
        /// </summary>
        public string Title
        {
            get { return "PrimalityTest"; }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            input = "0";
            result = "";
            isEnable = true;
            btnText = "Calculation";
            Reps = 23;
        }

        #endregion

        #region Property
        private int reps;
        /// <summary>
        /// Miller-Rabin loop count
        /// </summary>
        public int Reps
        {
            get { return reps; }
            set
            {
                Set(() => Reps, ref reps, value);
                Accuracy = 1 - 1 / Math.Pow(4, Reps);
            }
        }

        private double accuracy;
        /// <summary>
        /// probability of the number is 'probably' prime.
        /// </summary>
        public double Accuracy
        {
            get { return accuracy; }
            set { Set(() => Accuracy, ref accuracy, value); }
        }

        private string input;
        /// <summary>
        /// input data
        /// </summary>
        public string Input
        {
            get { return input; }
            set { Set(() => Input, ref input, value); }
        }

        private string result;
        /// <summary>
        /// result
        /// </summary>
        public string Result
        {
            get { return result; }
            set { Set(() => Result, ref result, value); }
        }

        private double runTime;
        /// <summary>
        /// run time(ms)
        /// </summary>
        public double RunTime
        {
            get { return runTime; }
            set { Set(() => RunTime, ref runTime, value); }
        }

        private bool isEnable;
        /// <summary>
        /// calc button is enable 
        /// </summary>
        public bool IsEnable
        {
            get { return isEnable; }
            set { Set(() => IsEnable, ref isEnable, value); }
        }

        private string btnText;
        /// <summary>
        /// calc button text
        /// </summary>
        public string BtnText
        {
            get { return btnText; }
            set { Set(() => BtnText, ref btnText, value); }
        }

        #endregion

        #region Commands

        public ICommand CalcPrime { get { return new RelayCommand(OnCalcPrime, () => true); } }

        private void OnCalcPrime()
        {
            /// TODO: AsyncCommand with Task
            ThreadPool.QueueUserWorkItem(new WaitCallback(_ =>
            {
                BtnText = "Processing";
                IsEnable = false;
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    double acc = 1;
                    bool res = BigInteger.Parse(input).IsPrime(reps, out acc); /// see BigIntegerEx.cs
                    Result = res ? "The probability that this number is prime is " + acc.ToString() : "This is NOT prime.";
                    RunTime = stopwatch.GetDoubleElapsedMilliseconds();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                IsEnable = true;
                BtnText = "Calculation";

            }));
        }

        #endregion
    }
}
